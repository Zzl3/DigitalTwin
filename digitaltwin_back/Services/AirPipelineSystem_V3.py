# -*- coding: utf-8 -*-
"""
Created on Tue Nov 15 16:54:57 2022

@author: DELL

"""

# 船厂空压管网系统初步建模

# 单管类引用 ：AirPipelineSystem_管网类框架.py
# 空压机类引用：AirPipelineSystem_V2.py
# 储气罐类引用：AirPipelineSystem_V2.py

from AirPipelineSystem_V2 import Compressor_class, AirTank_class,AirTank_2Ins_class, CalcSysPower
import math
import pandas as pd
import numpy as np
import CoolProp
from CoolProp.CoolProp import PhaseSI, PropsSI, get_global_param_string
from datetime import datetime,timedelta

from abc import ABC, abstractmethod

from Correlations import Density, Viscocity

class Pipe_class(ABC):
    #单管模型 抽象类
    
    def __init__(self):
        
        self.D=np.NaN                 #管内径，mm
        self.L=np.NaN                 #管长，m
        self.delta_H=np.NaN           #绝对高差，m；向上流动>0；向下流动<0
        self.k=np.NaN                 #管内粗糙度，mm
        self.zeta=np.NaN              #局部阻力系数
        
        self.Q_m = np.NaN             #质量流量，kg/s
        self.Q_v = np.NaN             #体积流量，m3/s
        
                                      #进口
        self.nodeIn_P=np.NaN          #绝对压力，kPa
        self.nodeIn_T=np.NaN          #温度，C
        self.nodeIn_rho=np.NaN        #密度，kg/m3
        self.nodeIn_mu=np.NaN         #动力黏度，Pa·s
        
        self.nodeOut_P=np.NaN         #出口
        self.nodeOut_T=np.NaN
        self.nodeOut_rho=np.NaN
        self.nodeOut_mu=np.NaN 
    
    def ResetPipe(self,D,L,k,delta_H,zeta):
        #设置管路尺寸数据
        
        self.D=D
        self.L=L
        self.k=k
        self.delta_H=delta_H
        self.zeta=zeta
    
    def ResetNodeIn(self,P,T,fluid):
        #计算进口节点物性
        
        self.nodeIn_P=P
        self.nodeIn_T=T
        
        self.nodeIn_rho = Density(self.nodeIn_P * 1000, 273.15+self.nodeIn_T)
        self.nodeIn_mu = Viscocity(273.15+self.nodeIn_T)
    
    def ResetNodeOut(self,P,T,fluid):
        #计算出口节点物性
        
        self.nodeOut_P=P
        self.nodeOut_T=T
        
        self.nodeOut_rho = Density(self.nodeOut_P * 1000, 273.15+self.nodeOut_T)
        self.nodeOut_mu = Viscocity(273.15+self.nodeOut_T)
    
    def ResetFlowRate(self,Q_m):
        self.Q_m=Q_m
    
    @abstractmethod
    def CalcPressDrop(self):
        #压降计算抽象函数
        #子类必须重构
        pass
    
class PipeAir_class(Pipe_class):
    #空气单管模型
    
    def __init__(self):
        
        super().__init__()
        
        self.fluid = 'Air'
        
    def CalcPressDrop(self, size):
        #分段法计算压降: 按管长等分size段
        #每一段取进口侧的物性
        
        delta_P = 0
        L = self.L / size
        

        for i in range(size):
            if i == 0:
                velocity = self.Q_m / self.nodeIn_rho / (0.25 * 3.14 * (self.D/1000)**2)  # m/s
                rho = self.nodeIn_rho
                mu = self.nodeIn_mu
                
                Re = rho * velocity * (self.D/1000) / mu 
                
                lam = CalcLambda(Re, self.k, self.D)
                
                eqL = self.zeta * self.D / 1000 / lam / size
                
                delta_P = delta_P + lam * (L + eqL) / (self.D/1000) * 0.5 * rho * velocity**2 / 1000    # kPa
                p_in = self.nodeIn_P - delta_P
            else:
                
                rho = Density(p_in * 1000, 273.15+self.nodeIn_T)
                mu = Viscocity(273.15+self.nodeIn_T)
                
                velocity = self.Q_m / rho / (0.25 * 3.14 * (self.D/1000)**2)  # m/s

                Re = rho * velocity * (self.D/1000) / mu
                lam = CalcLambda(Re, self.k, self.D)
                
                eqL = self.zeta * self.D / 1000 / lam / size
                
                delta_P = delta_P + lam * (L + eqL) / (self.D/1000) * 0.5 * rho * velocity**2 / 1000    # kPa
                p_in = self.nodeIn_P - delta_P 
        
        self.nodeOut_P = self.nodeIn_P - delta_P


def CalcLambda(Re,k,D):
    # 计算沿程阻力系数
    # 采用"莫迪公式"   
    
    # Re: 雷诺数
    # k: 管壁绝对粗糙度
    # D: 管内径

    if Re <= 2000 :
        lam = 64 / Re
    else:
        lam = 0.0055 * (1 + (20000*k/D + 1e6 / Re)**(1/3)) 
    
    return lam



class SWSAirSystem_class():
    # 船厂空压系统模型
    # 1#、2#、4#空压站
    
    def __init__(self, PipeData):
        
        self.Q_ratio_12 = 0.5           # 供气比, Pipe2_12 / Total_User12
        self.Q_ratio_3_1 = 0.5          # 供气比, Comp0 向 Comp1
        
        self.PipeData = PipeData     #管网尺寸数据矩阵 np.array; 顺序与 PipeNameList对应
        self.PipeNameList = ['Pipe0_3','Pipe0_13','Pipe0_15','Pipe0_16','Pipe0_17','Pipe0_18',
                             'Pipe1_4','Pipe1_5','Pipe1_6','Pipe1_7','Pipe1_8',
                             'Pipe2_10','Pipe2_11','Pipe2_12',
                             'Pipe3_1','Pipe3_9',
                             'Pipe13_12','Pipe13_14']
        
        self.UserList = ['Pipe1_4','Pipe1_5','Pipe1_6','Pipe1_7','Pipe1_8','Pipe3_9',
                         'Pipe2_10','Pipe2_11','Pipe2_12','Pipe13_14',
                         'Pipe0_15','Pipe0_16','Pipe0_17','Pipe0_18']
        
        self.Comp0 = Compressor_class()
        self.Comp1 = Compressor_class() 
        self.Comp2 = Compressor_class()
        
        self.AirTank0 = AirTank_class()
        self.AirTank1 = AirTank_2Ins_class()
        self.AirTank2 = AirTank_class()
    
    
        #---根据 PipeData，设置管路尺寸数据---
        names = self.__dict__
        for i in range(np.size(self.PipeNameList)):
            names[self.PipeNameList[i]] = PipeAir_class()
            names[self.PipeNameList[i]].ResetPipe(PipeData[i,0], PipeData[i,1], PipeData[i,2], PipeData[i,3], PipeData[i,4])
        #----end----

    
    def MassBalance(self, Q_use, rho_suc):
        # 系统流量计算
        # Q_use: 当前时刻各用户的流量需求, np.array, 顺序与 UserList对应, 单位: m3/h
        # P_tank: 当前时刻储气罐压力
        
        
        #---根据Q_use分配每根管路的流量---
        names = self.__dict__
        
        #用户管路流量分配
        k = 0
        for i in range(np.size(self.UserList)):
            names[self.UserList[i]].ResetFlowRate(Q_use[k] / 3600 * rho_suc)
            k = k+1
        
        #其他管路流量分配
        Q_m_area_Comp1 = self.Pipe1_4.Q_m + self.Pipe1_5.Q_m + self.Pipe1_6.Q_m + self.Pipe1_7.Q_m + self.Pipe1_8.Q_m
        self.Pipe3_1.ResetFlowRate(Q_m_area_Comp1 * self.Q_ratio_3_1)
        
        self.Pipe0_3.ResetFlowRate(self.Pipe3_1.Q_m + self.Pipe3_9.Q_m)
        
        self.Pipe2_12.ResetFlowRate(self.Pipe2_12.Q_m * self.Q_ratio_12)
        self.Pipe13_12.ResetFlowRate(self.Pipe2_12.Q_m / self.Q_ratio_12 * (1-self.Q_ratio_12))
        
        self.Pipe0_13.ResetFlowRate(self.Pipe13_12.Q_m + self.Pipe13_14.Q_m)
        #----end----

        return
    
    def CalcPressure(self, P_tank, iter_press, Set_Comp, T_Pipe):
        # P_tank, 每个储气罐压力, np.array, 顺序：AirTank0, AirTank1, AirTank2
        # Set_Comp, 每个空压站是否开启, np.array
        
        fluld = 'Air'
        
        #---根据P_tank, 计算每根管路的压降---
        #---Comp0 区域----
        self.Pipe0_3.ResetNodeIn(P_tank[0], T_Pipe, fluld)
        self.Pipe0_3.CalcPressDrop(iter_press)
        
        self.Pipe0_13.ResetNodeIn(P_tank[0], T_Pipe, fluld)
        self.Pipe0_13.CalcPressDrop(iter_press)
        
        self.Pipe0_15.ResetNodeIn(P_tank[0], T_Pipe, fluld)
        self.Pipe0_15.CalcPressDrop(iter_press)
        
        self.Pipe0_16.ResetNodeIn(P_tank[0], T_Pipe, fluld)
        self.Pipe0_16.CalcPressDrop(iter_press)
        
        self.Pipe0_17.ResetNodeIn(P_tank[0], T_Pipe, fluld)
        self.Pipe0_17.CalcPressDrop(iter_press)
        
        self.Pipe0_18.ResetNodeIn(P_tank[0], T_Pipe, fluld)
        self.Pipe0_18.CalcPressDrop(iter_press)
        
        self.Pipe13_12.ResetNodeIn(self.Pipe0_13.nodeOut_P, T_Pipe, fluld)
        self.Pipe13_12.CalcPressDrop(iter_press)
        
        self.Pipe13_14.ResetNodeIn(self.Pipe0_13.nodeOut_P, T_Pipe, fluld)
        self.Pipe13_14.CalcPressDrop(iter_press)
        
        self.Pipe3_1.ResetNodeIn(self.Pipe0_3.nodeOut_P, T_Pipe, fluld)
        self.Pipe3_1.CalcPressDrop(iter_press)
        
        self.Pipe3_9.ResetNodeIn(self.Pipe0_3.nodeOut_P, T_Pipe, fluld)
        self.Pipe3_9.CalcPressDrop(iter_press)
        
        #---Comp1 区域----
        self.Pipe1_4.ResetNodeIn(P_tank[1], T_Pipe, fluld)
        self.Pipe1_4.CalcPressDrop(iter_press)
        
        self.Pipe1_5.ResetNodeIn(P_tank[1], T_Pipe, fluld)
        self.Pipe1_5.CalcPressDrop(iter_press)
        
        self.Pipe1_6.ResetNodeIn(P_tank[1], T_Pipe, fluld)
        self.Pipe1_6.CalcPressDrop(iter_press)
        
        self.Pipe1_7.ResetNodeIn(P_tank[1], T_Pipe, fluld)
        self.Pipe1_7.CalcPressDrop(iter_press)
        
        self.Pipe1_8.ResetNodeIn(P_tank[1], T_Pipe, fluld)
        self.Pipe1_8.CalcPressDrop(iter_press)
        
        #---Comp2 区域----
        self.Pipe2_10.ResetNodeIn(P_tank[2], T_Pipe, fluld)
        self.Pipe2_10.CalcPressDrop(iter_press)
        
        self.Pipe2_11.ResetNodeIn(P_tank[2], T_Pipe, fluld)
        self.Pipe2_11.CalcPressDrop(iter_press)
        
        self.Pipe2_12.ResetNodeIn(P_tank[2], T_Pipe, fluld)
        self.Pipe2_12.CalcPressDrop(iter_press)
        #----end----
        return
    
    def AirTankBalance(self, P_tank, Set_Comp, T_tank, timestep):
        # 下一时刻储气罐压力计算
        # Q_use: 当前时刻各用户的流量需求, np.array, 顺序与 UserList对应
        # P_tank: 当前时刻储气罐压力, np.array
        # Set_Comp: 当前时刻空压机开停, np.array
        
        # ---1#站储气罐----
        Qm_out_tank0 = self.Pipe0_3.Q_m + self.Pipe0_13.Q_m + self.Pipe0_15.Q_m + self.Pipe0_16.Q_m + self.Pipe0_17.Q_m + self.Pipe0_18.Q_m
        
        self.AirTank0.ResetNodeIn(self.Comp0.nodeOut_P, T_tank)
        if Set_Comp[0]==1:
            self.AirTank0.ResetNodeInFlowRate(self.Comp0.Q_m) # 充气+放气
        else:
            self.AirTank0.ResetNodeInFlowRate(0)              # 仅放气
        
        self.AirTank0.T=T_tank
        self.AirTank0.ResetNodeOut(T_tank)
        self.AirTank0.ResetNodeOutFlowRate(Qm_out_tank0)
        
        self.AirTank0.CalcP(60*timestep)
        
        # ---2#站储气罐----
        Qm_out_tank1 = self.Pipe1_4.Q_m + self.Pipe1_5.Q_m + self.Pipe1_6.Q_m + self.Pipe1_7.Q_m + self.Pipe1_8.Q_m
        
        self.AirTank1.ResetNodeIn(self.Comp1.nodeOut_P, T_tank)
        self.AirTank1.ResetNodeIn2(self.Pipe3_1.nodeOut_P, T_tank)

        self.AirTank1.ResetNodeIn2FlowRate(self.Pipe3_1.Q_m)
        if Set_Comp[1]==1:
            self.AirTank1.ResetNodeInFlowRate(self.Comp1.Q_m) # 充气+放气
        else:
            self.AirTank1.ResetNodeInFlowRate(0)              # 仅放气
        
        self.AirTank1.T=T_tank
        self.AirTank1.ResetNodeOut(T_tank)
        self.AirTank1.ResetNodeOutFlowRate(Qm_out_tank1)
        
        self.AirTank1.CalcP(60*timestep)
        
        # ---4#站储气罐----
        Qm_out_tank2 = self.Pipe2_10.Q_m + self.Pipe2_11.Q_m + self.Pipe2_12.Q_m
        
        self.AirTank2.ResetNodeIn(self.Comp2.nodeOut_P, T_tank)
        if Set_Comp[2]==1:
            self.AirTank2.ResetNodeInFlowRate(self.Comp2.Q_m) # 充气+放气
        else:
            self.AirTank2.ResetNodeInFlowRate(0)              # 仅放气
        
        self.AirTank2.T=T_tank
        self.AirTank2.ResetNodeOut(T_tank)
        self.AirTank2.ResetNodeOutFlowRate(Qm_out_tank2)
        
        self.AirTank2.CalcP(60*timestep)
        
        return
    
    
    def Print(self):
        return
      
