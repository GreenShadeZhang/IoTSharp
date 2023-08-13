docker run -p 3306:3306 --name mysql -e MYSQL_ROOT_PASSWORD=kissme -d mysql

Script-Migration 20230731134232_QinglanToken -context IoTSharp.Data.ApplicationDbContext

Add-Migration UpdateDevice -context IoTSharp.Data.ApplicationDbContext

Update-Database UpdateDevice -context IoTSharp.Data.ApplicationDbContext