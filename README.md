
[![Build Status](https://dev.azure.com/suadev0095/docker-workshop/_apis/build/status/suadev.docker-workshop-with-react-aspnetcore-redis-rabbitmq-mssql?branchName=master)](https://dev.azure.com/suadev0095/docker-workshop/_build/latest?definitionId=1&branchName=master)


**Running with Docker-Compose**

- In the root directory of the project run "**docker-compose up**" command.
- Wait all containers to start.
- Browse http://localhost:5001

**Notes**
- api service keeps waiting till mssql container ready to accept connections. This waiting feature was implemented with wait-for-it.
(https://github.com/vishnubob/wait-for-it)
- redis and rabbitmq containers are faster than mssql container on starting and accepting connections.That's why api service waits only mssql.
- mssql, redis and rabbitmq volumes are exist and active by defauls in docker-compose.yml
- React hot-reloading is working on development environment.


## Sample Screencast

![alt text](https://github.com/suadev/docker-workshop-with-react-aspnetcore-redis-rabbitmq-mssql/blob/master/react_ui/public/screencast.gif)

### Overall Architecture

![alt text](https://github.com/suadev/docker-workshop-with-react-aspnetcore-redis-rabbitmq-mssql/blob/master/react_ui/public/docker_workshop.png)
