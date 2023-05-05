import { get } from '@/api/data';

export function getConsumption() {
  return get('/consumption/getDatas').then(response => {
    // 解析响应数据
    if (response.code === 200) {
      return response.data
    } else {
      throw new Error(response.msg || 'Unknown error');
    }
  });
}