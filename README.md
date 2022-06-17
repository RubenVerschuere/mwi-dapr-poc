# Dapr Poc MWI

## Pre requirements

- Dapr installed: https://docs.dapr.io/getting-started/install-dapr-cli/
- Docker desktop installed: https://docs.docker.com/desktop/windows/install/
- .NET 6 SDK



## Start Dapr applications

Run processor
```
dapr run --app-port 7001 --app-id order-processor-demo --config ./../config.yaml 
--app-protocol http --dapr-http-port 3501 -- dotnet run
```

Run checkout
```
dapr run --app-id checkout-demo --config ./../config.yaml --app-protocol http --dapr-http-port 3500 -- dotnet run
```

View all running applications

```
dapr dashboard
```

Check running containers in docker

```
docker ps

//Result
CONTAINER ID   IMAGE               COMMAND                  CREATED      STATUS                  PORTS                              NAMES
4ca6b919e4bb   daprio/dapr:1.7.4   "./placement"            2 days ago   Up 24 hours             0.0.0.0:6050->50005/tcp            dapr_placement
ba756230c85b   redis               "docker-entrypoint.s…"   3 days ago   Up 24 hours             0.0.0.0:6379->6379/tcp             dapr_redis
4223cd369377   openzipkin/zipkin   "start-zipkin"           3 days ago   Up 24 hours (healthy)   9410/tcp, 0.0.0.0:9411->9411/tcp   dapr_zipkin
```


## Check Redis store

Start Redis CLI

```
docker exec -it dapr_redis redis-cli
```

Check all the inserted keys

```
keys *
```

Get data from one key

```
hgetall "myapp||name"
```
