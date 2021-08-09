# azure.devops.notify.dingtalk.robots

docker-compose.yml
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

appsettings.json
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
      "secret": "xxx"
    }
  ],
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
```