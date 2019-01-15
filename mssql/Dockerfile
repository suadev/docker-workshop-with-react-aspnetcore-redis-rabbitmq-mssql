# --------- This Dockerfile will be using for restoring DB backup. Not ready for now. ------------

FROM mcr.microsoft.com/mssql/server

# ENV ACCEPT_EULA=Y
# ENV SA_PASSWORD=Brt_z!py

# CMD [ "/opt/mssql/bin/sqlservr" ]

# RUN apt-get update && apt-get install unzip -y

# # SqlPackage taken from https://github.com/Microsoft/mssql-docker/issues/135#issuecomment-389245587
# RUN wget -O sqlpackage.zip https://go.microsoft.com/fwlink/?linkid=873926 \
#     && unzip sqlpackage.zip -d /tmp/sqlpackage \
#     && chmod +x /tmp/sqlpackage/sqlpackage

# # sqlpackage .bak ları da açar mı?
# COPY database.bacpac /tmp/db/database.bacpac  

# # grep command to check if SQL server is already started taken from https://stackoverflow.com/a/51589787/488695
# RUN ( /opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" \
#     && /tmp/sqlpackage/sqlpackage /a:Import /tsn:localhost,1433 /tdn:Database /tu:sa /tp:Brt_z!py /sf:/tmp/db/database.bacpac \
#     && pkill sqlservr \
#     && rm /tmp/db/database.bacpac