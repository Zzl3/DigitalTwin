import axios from 'axios';


const baseURL = 'http://localhost:5000';
//const baseURL = 'http://124.223.160.61:5000'; //部署到服务器上面

const api = axios.create({
  baseURL: baseURL,
  timeout: 5000, // 请求超时时间
});

// 统一处理请求错误
api.interceptors.response.use(
  response => {
    return response.data;
  },
  error => {
    // 处理请求错误
    console.log(error);
    return Promise.reject(error);
  }
);

// 封装请求方法
export function get(url, params) {
  return api.get(url, { params });
}

export function post(url, data) {
  return api.post(url, data);
}
