import { get } from '@/api/data';

export function getPercent() {
  return get('/c++_left/getPercent').then(response => {
    // 解析响应数据
    if (response.code === 200) {
      return response.data
    } else {
      throw new Error(response.msg || 'Unknown error');
    }
  });
}

export function getNumber() {
    return get('/c++_left/getNumber').then(response => {
      // 解析响应数据
      if (response.code === 200) {
        return response.data
      } else {
        throw new Error(response.msg || 'Unknown error');
      }
    });
  }