### Run 3 Nodes:
```bash
docker-compose up
```

### Join Node 2 and Node 3:
```bash
rabbitmqctl stop_app
rabbitmqctl join_cluster rabbit@rabbitmq1
rabbitmqctl stop
rabbitmqctl start_app
```