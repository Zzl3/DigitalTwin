import { get,post } from '@/api/data';

export function getData() {
  return get('/dataManage/getDatas').then(response => {
    // 解析响应数据
    if (response.code === 200) {
      return response.data
    } else {
      throw new Error(response.msg || 'Unknown error');
    }
  });
}
  
export function addData(data) {
  return post('/dataManage/addData', data)
    .then(response => {
        if (response.code === 200) {
            return response.data
          } else {
            throw new Error(response.msg || 'Unknown error');
          }
    });
}
export const deleteData= (id) => {
    return post(`/dataManage/deleteData?id=${id}`)
      .then(response => {
        if (response.code === 200) {
            return response.data
          } else {
            throw new Error(response.msg || 'Unknown error');
          }
      })
      .catch(error => {
        // 处理请求失败时的错误信息
        console.log(error);
      });
  };
