![Docker Automated build](https://img.shields.io/docker/automated/luacloud/devops.to.dingtalk.robots)  ![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/luacloud/devops.to.dingtalk.robots)
# docker-compose.yml
```yaml
version: "2.0"
services:
  dingtalk:
    image: luacloud/devops.to.dingtalk.robots:latest
    container_name: dingtalk
    environment:
      - TZ:Asia/Shanghai
    volumes:
      - ./appsettings.json:/app/appsettings.json
    ports:
      - 80:80
```

# appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "robots": [
    {
      "Name": "DevOps通知群",
      "access_token": "xxx",
      "secret": "xxx",
      "Types": [ 
      {
            "Type":"PR",
            "WorkItemTypes":[],
      },
      {
            "Type":"Task",
            "WorkItemTypes":["用户情景","Bug","任务"],
      }],     
      "AtOnly": true,
      "UserMappings": [
        {
          "devops": "ABC",
          "dingtalk": "18000000001"
        },
        {
          "devops": "BCD",
          "dingtalk": "18000000003"
        },
        {
          "devops": "DEF",
          "dingtalk": "18000000002"
        }
      ]
    }
  ],
  "DefaultUserMappings": [
    {
      "devops": "ABC",
      "dingtalk": "18000000001"
    },
    {
      "devops": "BCD",
      "dingtalk": "18000000003"
    },
    {
      "devops": "DEF",
      "dingtalk": "18000000002"
    }
  ]
}
```

# robots

## Name

用来标识是哪一个通知机器人
## access_token

钉钉群机器人的token

## secret

为了安全性，所以只使用钉钉群机器人的secret

## Types

产生通知的类型，可以用来过滤是显示拉去请求的通知还是任务项的。
### Type

- PR: PR拉取请求
- Task: 工作项
### WorkItemTypes

目前仅针对工作项中的内容，包括["用户情景","Bug","任务"]
## AtOnly

- true: 只@工作项中提到的人
- false: @所有人
# DefaultUserMappings & UserMappings

当robots节点中的UserMappings为空集合的时候则使用DefaultUserMappings中的用户发起通知
## devops

AzureDevops中的用户名称

## dingtalk

dingtalk中的用户手机号，用来将AzureDevops中的用户在dingtalk显示@效果