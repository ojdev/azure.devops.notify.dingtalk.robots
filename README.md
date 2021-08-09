# azure.devops.notify.dingtalk.robots


```yaml
version: "2.0"
services:
  dingtalk:
    image: luacloud/devops.to.dingtalk.robots:latest
    container_name: dingtalk
    environment:
      - TZ=Asia/Shanghai
      - reboots:
        - :
          Name: DevOps通知群
          access_token: 机器人的token
          secret: 机器人的secret
      - UserMappings:
        - :
          devops: 钉钉中的人名，与azuredevops中的一致
          dingtalk: 钉钉里的手机号
    restart: always
    ports:
      - 18880:80
```