import { get } from '@/api/data';

export function getOnandoffs() {
  return get('/onandoff/getDatas').then(response => {
    // 解析响应数据
    if (response.code === 200) {
      return response.data
    } else {
      throw new Error(response.msg || 'Unknown error');
    }
  });
}

export function getnewOnandoffs() {
  return get('/onandoff/getnewDatas').then(response => {
    // 解析响应数据
    if (response.code === 200) {
      return response.data
    } else {
      throw new Error(response.msg || 'Unknown error');
    }
  });
}