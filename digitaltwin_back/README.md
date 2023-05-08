# 外灶村项目后端

采用的技术栈为 dotNet7, Asp.Net core, EntityFramework, StackExchange.Redis。数据库使用了MySQL和Redis，

Json解析目前使用的是dotnet内置的System.Text.Json

API测试工具为swagger

鉴权授权方案为双JWT Token的方案，用户登录返回accessToken和refreshToken，refreshToken的有效期为accessToken的两倍，前端每次请求在header中加上accessToken，当accessToken过期时，前端使用refreshToken刷新两个Token，目前写好的API为登录/登出/刷新Token三个，API返回数据的格式如下

code 自定义状态码可以在[Utility/ResponseStatusCode.cs](./Utility/ResponseStatusCode.cs)文件中添加

```json
{
    "code":"状态码，可以自定义，方便前端判断请求的状态",
    "msg":"详细信息等",
    // data字段返回真正的数据
    "data":{}
}
```

## `/auth/login`

**POST**

用户登录API，所需参数`phone:string`,`password:string`,以form表单的形式

返回数据格式

`phone`和`password`都正确的话：
```json
{
    "code":200,
    "msg":"",
    "data":{
        "value1":"{accessToken}", // accessToken
        "value2":"{refreshToken}",// refreshToken
        "userInfo":{
            // 用户的个人信息
        }
    }
}
```

其余情况`data`均为`null`

```json
{
    "code":500,
    "msg":"Internal error."
}
```

```json
{

    "code": 4011,
    "msg" : "Phone is not valid."
}
```

```json
{
    "code":4012,
    "msg":"Password is not valid."
}
```

## `/auth/logout`

**POST**

用户登出API

所需参数`refreshToken`，以form表单的形式

返回格式
```json
{
    "code":200,
    "msg":"success",
    "data":null
}
```

## `/auth/refresh`

**POST**

刷新Token

所需参数`refreshToken`，以form表单的形式

返回格式
```json
{
    "code":200,
    "msg":"success",
    "data":{
        "newAccess":"{accessToken}", // accessToken
        "newRefresh":"{refreshToken}",// refreshToken
    }
}
```

**整体项目结构有点烂，大家凑合写吧**