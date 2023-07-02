# -*- coding: utf-8 -*-
"""
Created on Fri Sep  2 12:21:08 2022

@author: DELL
"""

# 用于压缩空气管网的初步建模

import math
import pandas as pd
import numpy as np
import CoolProp
from CoolProp.CoolProp import PhaseSI, PropsSI, get_global_param_string
from datetime import datetime,timedelta

from Correlations import Density, Viscocity

class Pipe_class():
    #单管模型
    
    def __init__(self):
        
        self.D=np.NaN                 #管内径，mm
        self.L=np.NaN                 #管长，m
        self.delta_H=np.NaN           #绝对高差，m；向上流动>0；向下流动<0
        self.k=np.NaN                 #管内粗糙度，mm
        self.zeta=np.NaN              #局部阻力系数
        
        self.Q_m = np.NaN             #质量流量，kg/s
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
        self.D=D
        self.L=L
        self.k=k
        self.delta_H=delta_H
        self.zeta=zeta
        
    def ResetFlowRate(self,Q_m):
        self.Q_m=Q_m

    
    def ResetNodeIn(self,P,T):
        self.nodeIn_P=P
        self.nodeIn_T=T

        self.nodeIn_rho = Density(self.nodeIn_P * 1000, 273.15+self.nodeIn_T)
        self.nodeIn_mu = Viscocity(273.15+self.nodeIn_T)
    
    def ResetNodeOut(self,P,T):
        self.nodeOut_P=P
        self.nodeOut_T=T
        
        self.nodeOut_rho = Density(self.nodeOut_P * 1000, 273.15+self.nodeOut_T)
        self.nodeOut_mu = Viscocity(273.15+self.nodeOut_T)

 
    def Balance(self):
        # 整体法计算压降: 将进口密度作为整根管的密度，且保持不变
        
        velocity = self.Q_m / self.nodeIn_rho / (0.25 * 3.14 * (self.D/1000)**2)  # m/s
        
        Re = self.nodeIn_rho * velocity * (self.D/1000) / self.nodeIn_mu 
        
        lam = CalcLambda(Re,self.k,self.D)
        
        eqL = self.zeta * self.D / 1000 / lam   #当量长度
        
        delta_P = lam * (self.L + eqL) / (self.D/1000) * 0.5 * self.nodeIn_rho * velocity**2 / 1000    # kPa
        
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


class Compressor_class():
    #空压机能耗模型
    #效率模型(等熵压缩 + 等熵效率)
    
    def __init__(self):
        
        self.Q_m=np.NaN          #质量流量，kg/s
        self.Q_v=np.NaN          #体积流量，m3/s        
        self.Power=np.NaN        #功耗，kW
        self.N=np.NaN            #转速，rpm
        self.eta_s=np.NaN        #等熵效率
        
                                      #进口
        self.nodeIn_P=np.NaN          #绝对压力，kPa
        self.nodeIn_T=np.NaN          #温度，C
        self.nodeIn_rho=np.NaN        #密度，kg/m3
        self.nodeIn_mu=np.NaN         #动力黏度，Pa·s
        
        self.nodeOut_P=np.NaN         #出口
        self.nodeOut_T=np.NaN
        self.nodeOut_rho=np.NaN
        self.nodeOut_mu=np.NaN   

    def ResetNodeIn(self,P,T):
        #设置进口物性
        
        self.nodeIn_P=P
        self.nodeIn_T=T
        
        self.nodeIn_rho = Density(self.nodeIn_P * 1000, 273.15+self.nodeIn_T)
        self.nodeIn_mu = Viscocity(273.15+self.nodeIn_T)
    
    def ResetNodeOut(self,P,T):
        #设置出口物性
        
        self.nodeOut_P=P
        self.nodeOut_T=T
        
        self.nodeOut_rho = Density(self.nodeOut_P * 1000, 273.15+self.nodeOut_T)
        self.nodeOut_mu = Viscocity(273.15+self.nodeOut_T)
    
    def ResetFlowRate(self,Q_v):
        # 设置流量
        
        self.Q_v=Q_v
        self.Q_m = self.Q_v * self.nodeIn_rho


class AirTank_class():
    #储气罐模型 (单充气源)
    #理想气体
    #等温充放气
    
    def __init__(self):
        
        self.P=np.NaN              #储气压力，kPa
        self.V=np.NaN              #容积，m3        
        self.T=np.NaN              #温度，C
        self.rho=np.NaN            #密度，kg/m3
        
        self.delta_t=np.NaN         #时间步长，s
        
                                      #进口
        self.nodeIn_P=np.NaN          #充气压力，kPa
        self.nodeIn_T=np.NaN          #温度，C
        self.nodeIn_rho=np.NaN        #密度，kg/m3
        self.nodeIn_Qm=np.NaN         #质量流量，kg/s
        self.nodeIn_Qv=np.NaN         #体积流量，m3/s
        
        self.nodeOut_P=np.NaN               #出口
        self.nodeOut_T=np.NaN 
        self.nodeOut_rho=np.NaN
        self.nodeOut_Qm=np.NaN
        self.nodeOut_Qv=np.NaN
        
    def ResetTank(self,P,V,T):
        #设置储气罐物性
        
        self.P=P
        self.V=V
        self.T=T
        
        self.rho = Density(self.P * 1000, 273.15+self.T)
        
        self.nodeOut_P=P               #出口


    def ResetNodeIn(self,P,T):
        #设置进口物性
        
        self.nodeIn_P=P
        self.nodeIn_T=T
        
        self.nodeIn_rho = Density(self.nodeIn_P * 1000, 273.15+self.nodeIn_T)
    
    def ResetNodeOut(self,T):
        #设置出口物性

        self.nodeOut_T=T
        
        self.nodeOut_rho = Density(self.nodeOut_P * 1000, 273.15+self.nodeOut_T)
    
    def ResetNodeInFlowRate(self,Q_m):
        # 设置进口流量
        
        self.nodeIn_Qm=Q_m
        self.nodeIn_Qv = self.nodeIn_Qm / self.nodeIn_rho
        
    def ResetNodeOutFlowRate(self,Q_m):
        # 设置出口流量
        
        self.nodeOut_Qm=Q_m
        self.nodeOut_Qv = self.nodeOut_Qm / self.nodeOut_rho  
    
    def CalcP(self,delta_t):
        # 计算下一时刻的储气罐压力
        self.delta_t = delta_t
        
        self.P = (self.nodeIn_P * self.nodeIn_Qv * self.delta_t + self.P * self.V - self.nodeOut_P * self.nodeOut_Qv * self.delta_t) / self.V
        self.nodeOut_P = self.P  #放气压力 = 储气压力

class AirTank_2Ins_class(AirTank_class):
    #储气罐模型 (双充气源)
    #理想气体
    #等温充放气
    def __init__(self):
        super().__init__()
                                       #进口2
        self.nodeIn2_P=np.NaN          #充气压力，kPa
        self.nodeIn2_T=np.NaN          #温度，C
        self.nodeIn2_rho=np.NaN        #密度，kg/m3
        self.nodeIn2_Qm=np.NaN         #质量流量，kg/s
        self.nodeIn2_Qv=np.NaN         #体积流量，m3/s
    
    def ResetNodeIn2(self,P,T):
        #设置进口2物性
        
        self.nodeIn2_P=P
        self.nodeIn2_T=T
        
        self.nodeIn2_rho = Density(self.nodeIn2_P * 1000, 273.15+self.nodeIn2_T)
    
    def ResetNodeIn2FlowRate(self,Q_m):
        # 设置进口2流量
        
        self.nodeIn2_Qm=Q_m
        self.nodeIn2_Qv = self.nodeIn2_Qm / self.nodeIn2_rho
    
    def CalcP(self,delta_t):
        # 计算下一时刻的储气罐压力
        self.delta_t = delta_t
        
        self.P = (self.nodeIn_P * self.nodeIn_Qv * self.delta_t + self.nodeIn2_P * self.nodeIn2_Qv * self.delta_t + self.P * self.V - self.nodeOut_P * self.nodeOut_Qv * self.delta_t) / self.V
        self.nodeOut_P = self.P  #放气压力 = 储气压力
    
    
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

def CalcSysPower(Q_m,p_in,T_in,eta_s,p_out):
    # 计算系统能耗(空压机能耗)
    
    # Q_m: 空气质量流量, kg/s
    # p_in: 吸气压力, kPa
    # p_out: 排气压力, kPa
    # T_in: 吸气温度, C
    # eta_s: 等熵效率
    # Power: 压缩能耗, kW
        
    w_0 = 287 * (T_in +273.15) * 1.4 / (1.4-1) *((p_out/p_in)**((1.4-1)/1.4) - 1) / 1000
    
    Power = Q_m * w_0 / eta_s

    return Power



def ConditionTableLoading(ConditionTimeTable,Time_hour):
    #根据小时工况表读取当前工况，返回吸气温度和每个用户的用气量 m3/h
    
    # [时, 吸气温度, 用户1负荷,……, 用户n负荷]
    # ConditionTable=np.array([[0, 20, 3948, 1595, 60, 4123, 1160, 5040, 421, 360],
    #                          [1, 20, 3948, 1276, 48, 3298, 928, 4032, 336, 228]])
    
    T_suc = ConditionTimeTable[int(Time_hour), 1]
    Q_use = ConditionTimeTable[int(Time_hour), 2:]
            
    return T_suc,Q_use


def StrategyTimetableSetting(StrategyTimeTable,Time_hour,Time_minute,P,Set_ap_main):
    #读取策略表并进行设置的函数，输出补气设定
    #StrategyTimeTable-策略时间表，格式：[时间_小时，时间_分钟，开始补气储气罐压力，结束补气储气罐压力; ...]
    # StrategyTimeTable=np.array([[0, 00, 850, 950],
    #                             [0, 10, 800, 950]])
    
    #P-为控制储气罐内的压力
    #Set_ap_main-为空压机补气的设定，为总开关控制
    
    CurrentTime=Time_hour+Time_minute/60;
    
    
    #根据时间读取当前策略   
    for i in range(np.size(StrategyTimeTable,axis=0)):
        if (StrategyTimeTable[i,0]+StrategyTimeTable[i,1]/60)<=CurrentTime:
            if i==np.size(StrategyTimeTable,axis=0)-1:
                CurrentStrategy=StrategyTimeTable[i,:] 
                break
            elif (StrategyTimeTable[i+1,0]+StrategyTimeTable[i+1,1]/60)>CurrentTime:
                CurrentStrategy=StrategyTimeTable[i,:]
                break
        else: #第一次补气前全关
            return 0

    #根据策略设定空压机开关
    if Set_ap_main==0 and P<CurrentStrategy[2]:
        Set_ap_main=1
    elif Set_ap_main==1 and P>CurrentStrategy[3]:
        Set_ap_main=0
        
    return Set_ap_main

# def StrategyTimetableSetting_V2(StrategyTimeTable,Time_hour,Time_minute,P,Set_ap_main,isSupEnded):
#     #读取策略表并进行设置的函数，输出补气设定
#     # StrategyTimeTable-策略时间表，格式：[时间_小时, 时间_分钟, 目标储气罐压力; ...]
#     # StrategyTimeTable=np.array([[0, 10, 850],
#     #                             [0, 30, 900]])
    
#     # P-为当前时刻储气罐内的压力
#     # Set_ap_main-为空压机补气的设定，为总开关控制
    
#     CurrentTime=Time_hour+Time_minute/60;
    
    
#     #根据时间读取当前策略   
#     for i in range(np.size(StrategyTimeTable,axis=0)):
#         if (StrategyTimeTable[i,0]+StrategyTimeTable[i,1]/60)<=CurrentTime:
#             if i==np.size(StrategyTimeTable,axis=0)-1:    #最后一条策略之后
#                 CurrentStrategy=StrategyTimeTable[i,:]
#                 if isSupEnded[i]:                         #防止当前策略补气结束后，在下一条策略之前再出现补气的情况
#                     return 0
#                 else:
#                     if Set_ap_main==0 and P<CurrentStrategy[2]:
#                             Set_ap_main=1
#                     elif Set_ap_main==1 and P>CurrentStrategy[2]:
#                             Set_ap_main=0
#                             isSupEnded[i] = True
#                 break
#             elif (StrategyTimeTable[i+1,0]+StrategyTimeTable[i+1,1]/60)>CurrentTime:
#                 CurrentStrategy=StrategyTimeTable[i,:]
#                 if isSupEnded[i]:
#                     return 0
#                 else:
#                     if Set_ap_main==0 and P<CurrentStrategy[2]:
#                             Set_ap_main=1
#                     elif Set_ap_main==1 and P>CurrentStrategy[2]:
#                             Set_ap_main=0
#                             isSupEnded[i] = True
#                 break
#         else: #第一条策略之前
#             return Set_ap_main

        
#     return Set_ap_main

def StrategyTimetableSetting_V2(StrategyTimeTable, Time_hour, Time_minute, P, Set_ap_main, isSupEnded):
    #读取策略表并进行设置的函数，输出补气设定
    # StrategyTimeTable-策略时间表，格式：[时间_小时, 时间_分钟, 目标储气罐压力; ...]
    # StrategyTimeTable=np.array([[0, 10, 850],
    #                             [0, 30, 900]])
    
    # P-为当前时刻储气罐内的压力
    # Set_ap_main-为空压机补气的设定，为总开关控制

    CurrentTime = Time_hour + Time_minute / 60
    AllStrategyTime = StrategyTimeTable[:, 0] + StrategyTimeTable[:, 1] / 60
    num, dim = StrategyTimeTable.shape

    ind = np.searchsorted(AllStrategyTime, CurrentTime, 'right') - 1         # 找到最后一个小于等于CurrentTime的时间
    if (ind == num - 1) or (ind >= 0):
        CurrentStrategy = StrategyTimeTable[int(ind), :]
        if isSupEnded[int(ind)]:  # 防止当前策略补气结束后，在下一条策略之前再出现补气的情况
            return 0
        else:
            if Set_ap_main == 0 and P < CurrentStrategy[2]:
                Set_ap_main = 1
            elif Set_ap_main == 1 and P > CurrentStrategy[2]:
                Set_ap_main = 0
                isSupEnded[int(ind)] = True
        
    return Set_ap_main


