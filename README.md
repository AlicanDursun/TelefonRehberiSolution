# TelefonRehberiSolution

1- Solution dizininde komut satırı açarak docker-compose build komutunu çalıştırın.

2- Build işlemi bittikten sonra docker-compose up komutunu çalıştırın.

3- Proje ayağa kalktıktan sonra ister 5000 portundan apigateway üzerinden microservislere erişin
yad aşağıdaki microservis portlarından iletişim sağlayabilirsiniz.



----------SQLSERVER----------

ServerName: ipconfig v4 ,1433

Login: sa

Password: password@12345#


----------Api Gateway----------

http://localhost:5000/swagger/index.html

1- Detaysız Kişi listesi(Get)                      :  http://localhost:5000/Person/persons 

2- Detaylı Kişi listesi(Get)                       :  http://localhost:5000/Person/personwithdetails

3- Kişi Silme(Delete)                              :  http://localhost:5000/Person/personremove?id=92edf2e8-4a15-4635-afb4-a4c445e5a47b (gibi)

4- Kişi Ekleme(Post)                               :  http://localhost:5000/Person/person

Örnek Body json

{ "name": "string","surName": "string","company": "string"}
        
5- Tek Kişi Getirme(Get)                           :  http://localhost:5000/Person/person/9859a2ae-c2ed-4da4-8d46-d8d02ad6603d  

6- Kişi Detay Bilgisi Ekleme(Post)                 :  http://localhost:5000/PersonInformation/personInformation

Örnek Body json

{ "phoneNumber": "string","email": "string","location": "string","content": "string","personId": "d0b3eac4-f86d-4658-a160-620bcd6c3fe9"}

7- Kişi Detay Bilgileri Silme                      :  http://localhost:5000/PersonInformation/personInformationremove?id=adb46b8d-34ac-467a-dda3-08daef6e44f3 (gibi)

8- Konum Raporları Getirme(Get)                    :  http://localhost:5000/Report/reportsGetEvent?location=antalya 

9- Konum Raporu için Rabbitmq isteği(Get)          :  http://localhost:5000/Report/reportsGetList


----------Contact Api----------

Aşağıdaki linkten methodları ve içeriklerini görebilirsiniz.

http://localhost:5001/swagger/index.html

1- Detaysız Kişi listesi(Get)                      :  http://localhost:5001/api/Person/persons 

2- Detaylı Kişi listesi(Get)                       :  http://localhost:5001/api/Person/personwithdetails

3- Kişi Silme(Delete)                              :  http://localhost:5001/api/Person/personremove?id=92edf2e8-4a15-4635-afb4-a4c445e5a47b (gibi)

4- Kişi Ekleme(Post)                               :  http://localhost:5001/api/Person/person
        
5- Tek Kişi Getirme(Get)                           :  http://localhost:5001/api/Person/person/9859a2ae-c2ed-4da4-8d46-d8d02ad6603d  

6- Kişi Detay Bilgisi Ekleme(Post)                 :  http://localhost:5001/api/PersonInformation/personInformation

7- Kişi Detay Bilgileri Silme                      :  http://localhost:5001/api/PersonInformation/personInformationremove?id=adb46b8d-34ac-467a-dda3-08daef6e44f3 (gibi)




----------Report Api----------

Aşağıdaki linkten methodları ve içeriklerini görebilirsiniz.

http://localhost:5002/swagger/index.html

8- Konum Raporları Getirme(Get)                    :  http://localhost:5002/api/Report/reportsGetEvent?location=antalya 

9- Konum Raporu için Rabbitmq isteği(Get)          :  http://localhost:5002/api/Report/reportsGetList
