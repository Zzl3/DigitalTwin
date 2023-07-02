import { get,post } from '@/api/data';

export function getData() {
  return get('/runstrategy/getDatas').then(response => {
    // 解析响应数据
    if (response.code === 200) {
      return response.data
    } else {
      throw new Error(response.msg || 'Unknown error');
    }
  });
}

export function upgradeData(data) {
  return post('/runstrategy/upgradeData', data)
    .then(response => {
        if (response.code === 200) {
            return response.data
          } else {
            throw new Error(response.msg || 'Unknown error');
          }
    });
}

export function upgradeBestData() {
  return get('/runstrategy/upgradeBestData')
    .then(response => {
        if (response.code === 200) {
            return response.data
          } else {
            throw new Error(response.msg || 'Unknown error');
          }
    });
}