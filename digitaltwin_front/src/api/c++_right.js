import { get } from '@/api/data';

export function getStress() {
  return get('/c++_right/getStress').then(response => {
    // 解析响应数据
    if (response.code === 200) {
      return response.data
    } else {
      throw new Error(response.msg || 'Unknown error');
    }
  });
}

export function getWaterAndElectricity() {
    return get('/c++_right/getWaterAndElectricity').then(response => {
      // 解析响应数据
      if (response.code === 200) {
        return response.data
      } else {
        throw new Error(response.msg || 'Unknown error');
      }
    });
  }

  export function getNowElectricity() {
    return get('/c++_right/getNowElectricity').then(response => {
      // 解析响应数据
      if (response.code === 200) {
        return response.data
      } else {
        throw new Error(response.msg || 'Unknown error');
      }
    });
  }