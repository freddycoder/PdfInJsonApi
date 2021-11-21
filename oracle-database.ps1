# Download the lastest version of the oracle database image from dockerhub
docker pull oracleinanutshell/oracle-xe-11g

# Create a volume to store the database files
docker volume create --name=oracle-xe-11g

# Start the database
docker run --name=oracle-xe-11g -e ORACLE_PWD=oracle -p 1521:1521 -v oracle-xe-11g:/u01/app/oracle/oradata -d oracleinanutshell/oracle-xe-11g
