# -*- coding: utf-8 -*-
"""
Created on Thu Dec  1 18:50:07 2022

@author: DELL
"""

def Viscocity(T, T0=273.15, v0=1.7203362912807873e-05, B=110.4):
    #黏度计算
    #Sutherland 关联式
    # T-K

    return pow(T / T0, 1.5) * (T0 + B) / (T + B) * v0

def Density(P, T, R=287):
    #密度计算
    #理想气体方程
    # P-Pa
    # T-K
    
    return 1 / (R * T / P)