**Running with Docker-Compose**

- In the root directory of the project run "**docker-compose up**" command.
- Wait all containers to start.
- Browse React UI -> http://localhost:5001/

**P.S.**
All volumes (redis, rabbitmq, mssql) are commented out in docker-compose.yml. 

**Todo**
- React hot-loading for development environment
- Keep waiting api service untill rabbitmq and mssql completely start, by using custom shell script. 

## Sample Screencast

![alt text](https://github.com/suadev/docker-workshop-with-react-aspnetcore-redis-rabbitmq-mssql/blob/master/react_ui/public/screencast.gif)

### Overall Architecture

![alt text](https://github.com/suadev/docker-workshop-with-react-aspnetcore-redis-rabbitmq-mssql/blob/master/react_ui/public/docker_workshop.png)
