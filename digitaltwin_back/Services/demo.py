import sys

from AirPipelineSystem_V3 import PipeAir_class, SWSAirSystem_class, CalcLambda
from AirPipelineSystem_V2 import CalcSysPower, ConditionTableLoading, StrategyTimetableSetting_V2

import math
import pandas as pd
import numpy as np
from CoolProp.CoolProp import PhaseSI, PropsSI, get_global_param_string
from datetime import datetime,timedelta
from hyperopt import fmin,tpe,hp,STATUS_OK,STATUS_FAIL,Trials

import matplotlib
import matplotlib.pyplot as plt


min_P = 700                                 # 用户的最低供气压力 kPa            


# 流量表[时, 吸气温度, 用户1负荷,……, 用户n负荷]
ConditionTable=np.array([[0, 20, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240],
                         [1, 20, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240],
                         [2, 20, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240],
                         [3, 20, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240],
                         [4, 20, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240],
                         [5, 20, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960],
                         [6, 20, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960],
                         [7, 20, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960],
                         [8, 20, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840],
                         [9, 20, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840],
                         [10, 20, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840],
                         [11, 20, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840],
                         [12, 20, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960],
                         [13, 20, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960],
                         [14, 20, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840],
                         [15, 20, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840],
                         [16, 20, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840, 3840],
                         [17, 20, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960],
                         [18, 20, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960],
                         [19, 20, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960, 960],
                         [20, 20, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240],
                         [21, 20, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240],
                         [22, 20, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240],
                         [23, 20, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240, 240]])

PipeData=np.array([[300, 750, 0.005, 0, 3.4],
                   [150, 75, 0.005, 0, 2.7],
                   [120, 100, 0.005, 0, 0.7],
                   [120, 120, 0.005, 0, 0.7],
                   [120, 130, 0.005, 0, 0.7],
                   [120, 150, 0.005, 0, 0.7],
                   [125, 75, 0.005, 0, 0.7],
                   [125, 75, 0.005, 0, 0.7],
                   [125, 100, 0.005, 0, 0.7],
                   [125, 100, 0.005, 0, 0.7],
                   [125, 100, 0.005, 0, 0.7],
                   [150, 120, 0.005, 0, 0.7],
                   [150, 120, 0.005, 0, 0.7],
                   [100, 120, 0.005, 0, 2.7],
                   [300, 250, 0.005, 0, 2.7],
                   [120, 150, 0.005, 0, 3.4],
                   [120, 130, 0.005, 0, 4.7],
                   [120, 60, 0.005, 0, 2.7]])

    

#-----------------开始优化-----------------------


def CpdAirSystem_Simulation5(Strategy,System=SWSAirSystem_class(PipeData),Date=datetime(2022,12,6),eta_s=0.8, measure=False):
    
    S=Strategy
    Strategy_length=int((len(Strategy)-2)/2)
    for i in range(Strategy_length):
        Time=('Time'+str(i+1))
        Target=('Target'+str(i+1))
        if i==0:
            StrategyTimeTable = np.array([[int(S[Time]), int(60*(S[Time]-int(S[Time]))), S[Target]]])
        else:
            # 策略时间表改动
            S_insert=[[int(S[Time]), int(60*(S[Time]-int(S[Time]))), S[Target]]]
            StrategyTimeTable=np.insert(StrategyTimeTable, i, values=S_insert, axis=0)
            
    #从搜索值中，读取供气比
    System.Q_ratio_12 = S['Q_ratio_12']
    System.Q_ratio_3_1 = S['Q_ratio_3_1']
    
        # #约束1:时间顺序约束
        # else:
        #     if S[('Time'+str(i))]>=S[('Time'+str(i+1))]:
        #         return {'loss':100000,'status':STATUS_FAIL}
    
    #调用系统模型执行计算
    isSupEnded=np.zeros(shape=(Strategy_length))
    for i in range(Strategy_length):
        isSupEnded[i] = False
    
    
    # 空压机加卸载控制(空压机流量、排气压力固定)

    # 流量表[时, 吸气温度, 用户1负荷,……, 用户n负荷]
    # ConditionTable=np.array([[0, 20, 3948, 1595, 60, 4123, 1160, 5040, 421, 360],
    #                          [1, 20, 3948, 1276, 48, 3298, 928, 4032, 336, 228]])
    
    # 策略表[时, 分, 开始补气储气罐压力, 结束补气储气罐压力]
    # StrategyTimeTable=np.array([[0, 00, 850, 950],
    #                             [0, 10, 800, 950]])
    
    p_suc = 101.3    #吸气压力, kPa
    timestep=1       #时间步长/min，必须为60的因数，建议在小步长下运行，否则难以收敛
    iter_press = 1   #管网压降计算,分段数
    
    # 1#站
    Set_ap0_main=0      #开关
    Qv_ap0=60000        #流量, m3/h
    p_ap0=900           #排气压力, kPa
    V_tank0 = 120000     # 储气罐容积, m3    
    p0_tank0 = 750      # 初始时刻 储气罐压力, kPa
    
    # 2#站
    Set_ap1_main=0      
    Qv_ap1=27600       
    p_ap1=900           
    V_tank1 = 50000   
    p0_tank1 = 750
    
    # 4#站
    Set_ap2_main=0
    Qv_ap2=32400
    p_ap2=900
    V_tank2 = 50000
    p0_tank2 = 750 

    Time=Date       #初始时间，默认从当日零点开始计算，若需要改变则改变传入的Date
    
    
    # 分装每个空压站的策略表
    StrategyTimeTable_0 = np.array([StrategyTimeTable[0],StrategyTimeTable[1]])
    StrategyTimeTable_1 = np.array([StrategyTimeTable[2],StrategyTimeTable[3]])
    StrategyTimeTable_2 = np.array([StrategyTimeTable[4],StrategyTimeTable[5]]) 
    
    isSupEnded_0 = [isSupEnded[0],isSupEnded[1]]
    isSupEnded_1 = [isSupEnded[2],isSupEnded[3]]
    isSupEnded_2 = [isSupEnded[4],isSupEnded[5]]
    
    # 创建一系列数组用于存储数据
    iter_total=int(60*ConditionTable.shape[0]/timestep)
    
    P_tank0_arr=np.zeros(shape=(iter_total))
    P_tank1_arr=np.zeros(shape=(iter_total))
    P_tank2_arr=np.zeros(shape=(iter_total))
    
    Set_ap0_arr=np.zeros(shape=(iter_total))
    Set_ap1_arr=np.zeros(shape=(iter_total))
    Set_ap2_arr=np.zeros(shape=(iter_total))
    
    TIME=np.zeros(shape=(iter_total))
    
    P_user = [] #用户群供气压力数组
    for i in range(np.size(System.UserList)):    
        P_user.append(np.zeros(shape=(iter_total)))
        
    P_pipe = [] #管网出口压力数组
    deltaP_pipe = [] #管网压降数组
    for i in range(np.size(System.PipeNameList)):    
        P_pipe.append(np.zeros(shape=(iter_total)))
        deltaP_pipe.append(np.zeros(shape=(iter_total)))
    
    #---------初始化系统(空压机--储气罐--管网)------------------
    
    #---1#站----
    System.Comp0.Q_v = Qv_ap0 / 3600
    System.Comp0.ResetNodeIn(p_suc, ConditionTable[0,1])     # 空压机进口状态
    System.Comp0.ResetNodeOut(p_ap0, ConditionTable[0,1])    # 空压机出口状态
    System.Comp0.Q_m = System.Comp0.Q_v * System.Comp0.nodeIn_rho
    System.AirTank0.ResetTank(p0_tank0, V_tank0, ConditionTable[0,1]) #储气罐
    
    #---2#站----
    System.Comp1.Q_v = Qv_ap1 / 3600
    System.Comp1.ResetNodeIn(p_suc, ConditionTable[0,1])     # 空压机进口状态
    System.Comp1.ResetNodeOut(p_ap1, ConditionTable[0,1])    # 空压机出口状态
    System.Comp1.Q_m = System.Comp1.Q_v * System.Comp1.nodeIn_rho
    System.AirTank1.ResetTank(p0_tank1, V_tank1, ConditionTable[0,1]) #储气罐
    
    #---4#站----
    System.Comp2.Q_v = Qv_ap2 / 3600
    System.Comp2.ResetNodeIn(p_suc, ConditionTable[0,1])     # 空压机进口状态
    System.Comp2.ResetNodeOut(p_ap2, ConditionTable[0,1])    # 空压机出口状态
    System.Comp2.Q_m = System.Comp2.Q_v * System.Comp2.nodeIn_rho
    System.AirTank2.ResetTank(p0_tank2, V_tank2, ConditionTable[0,1]) #储气罐
    

    # 时间序列循环体    
    Pow=0                                           #总功耗
    Pow_Comp0 = 0
    Pow_Comp1 = 0
    Pow_Comp2 = 0
    Pow_list = []
    Pow_Comp0_list = []
    Pow_Comp1_list = []
    Pow_Comp2_list = []
    
    iter=0
    while Time < Date+timedelta(days=1):            #控制计算时间 days,hours
        
        #从储气罐处读取参数
        P_tank0=System.AirTank0.P
        P_tank1=System.AirTank1.P
        P_tank2=System.AirTank2.P

        #负荷表读取
        (T_suc,Q_use)=ConditionTableLoading(ConditionTable,Time.hour)
        
        #根据当前时间、策略表，判断空压机是否开启    
        Set_ap0_main=StrategyTimetableSetting_V2(StrategyTimeTable_0,Time.hour,Time.minute,P_tank0,Set_ap0_main, isSupEnded_0)
        Set_ap1_main=StrategyTimetableSetting_V2(StrategyTimeTable_1,Time.hour,Time.minute,P_tank1,Set_ap1_main, isSupEnded_1)
        Set_ap2_main=StrategyTimetableSetting_V2(StrategyTimeTable_2,Time.hour,Time.minute,P_tank2,Set_ap2_main, isSupEnded_2)
        
        #空压机计算
        rho_suc = PropsSI("D", "P", p_suc * 1000, "T", 273.15+T_suc, "Air")
        
        if Set_ap0_main==1:
            Q_m_comp0 = System.Comp0.Q_v * rho_suc
            dP = CalcSysPower(Q_m_comp0, p_suc, T_suc, eta_s, p_ap0) #kW
        else:
            dP=0                                                    #待机状态下,能耗水平
        Pow_Comp0 = Pow_Comp0 + dP*(timestep/60)
        
        if Set_ap1_main==1:
            Q_m_comp1 = System.Comp1.Q_v * rho_suc
            dP = CalcSysPower(Q_m_comp1, p_suc, T_suc, eta_s, p_ap1) #kW
        else:
            dP=0
        Pow_Comp1 = Pow_Comp1 + dP*(timestep/60)
        
        if Set_ap2_main==1:
            Q_m_comp2 = System.Comp2.Q_v * rho_suc
            dP = CalcSysPower(Q_m_comp2, p_suc, T_suc, eta_s, p_ap2) #kW
        else:
            dP=0
        Pow_Comp2 = Pow_Comp2 + dP*(timestep/60)
            
        Pow=Pow_Comp0 + Pow_Comp1 + Pow_Comp2                       #单位:千瓦时
        Pow_list.append(Pow)
        Pow_Comp0_list.append(Pow_Comp0)
        Pow_Comp1_list.append(Pow_Comp1)
        Pow_Comp2_list.append(Pow_Comp2)


        #管网计算
        #------------流量分配---------
        System.MassBalance(Q_use, rho_suc)
        
        #------------压降计算---------
        P_tank = np.array([P_tank0, P_tank1, P_tank2])
        Set_Comp = np.array([Set_ap0_main, Set_ap1_main, Set_ap2_main])
        System.CalcPressure(P_tank, iter_press, Set_Comp, T_suc)
        
        
        #储气罐计算
        System.AirTankBalance(P_tank, Set_Comp, T_suc, timestep)

        #记录序列数据
        P_tank0_arr[iter]=P_tank[0]
        P_tank1_arr[iter]=P_tank[1]
        P_tank2_arr[iter]=P_tank[2]
        
        Set_ap0_arr[iter]=Set_ap0_main
        Set_ap1_arr[iter]=Set_ap1_main
        Set_ap2_arr[iter]=Set_ap2_main
        
        names = System.__dict__         #用户群供气压力数组
        for i in range(np.size(System.UserList)):    
            P_user[i][iter] = names[System.UserList[i]].nodeOut_P
            
        #用户12供气压力为1#侧与4#侧的平均压力
        P_user[8][iter] = 0.5 * (System.Pipe2_12.nodeOut_P + System.Pipe13_12.nodeOut_P)
         
        for i in range(np.size(System.PipeNameList)):   
            P_pipe[i][iter] = names[System.PipeNameList[i]].nodeOut_P
            deltaP_pipe[i][iter] = names[System.PipeNameList[i]].nodeIn_P - names[System.PipeNameList[i]].nodeOut_P

        TIME[iter]=Time.hour+Time.minute/60 
        
        #时间步进
        Time=Time+timedelta(minutes=timestep)
        iter=iter+1            
        # 循环体结束
            
    return (Pow, Pow_Comp0, Pow_Comp1, Pow_Comp2,
            Set_ap0_arr, Set_ap1_arr, Set_ap2_arr,
            Pow_list, Pow_Comp0_list, Pow_Comp1_list, Pow_Comp2_list)



def main():
    """
     通过sys模块来识别参数demo, http://blog.csdn.net/ouyang_peng/
    """
    # print('参数个数为:', len(sys.argv), '个参数。')
    # print('参数列表:', str(sys.argv))
    # print('脚本名为：', sys.argv[0])
    # for i in range(1, len(sys.argv)):
    #     print('参数 %s 为：%s' % (i, sys.argv[i]))
    space={'Time1':float(sys.argv[1]),
       'Target1':float(sys.argv[2]),
       'Time2':float(sys.argv[3]),
       'Target2':float(sys.argv[4]),
       'Time3':float(sys.argv[5]),
       'Target3':float(sys.argv[6]),
       'Time4':float(sys.argv[7]),
       'Target4':float(sys.argv[8]),
       'Time5':float(sys.argv[9]),
       'Target5':float(sys.argv[10]),
       'Time6':float(sys.argv[11]),
       'Target6':float(sys.argv[12]),
       'Q_ratio_12':np.random.uniform(0,1),
       'Q_ratio_3_1':np.random.uniform(0,1)}
    result = CpdAirSystem_Simulation5(space)
    print(list(result[4]))
    print(list(result[5]))
    print(list(result[6]))
    print(result[7])
    


if __name__ == "__main__":
    print("<data>")
    main()
    print("</data>")