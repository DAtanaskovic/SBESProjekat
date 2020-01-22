## Sertifikati

1. Generisanje Self Signed sertifikata

   ```bash
    makecert -n "CN=ProjekatCA" -r -sv ProjekatCA.pvk ProjekatCA.cer
   ```

   Instalirati na Trusted Root Certification Authorities

2. Generisanje sertifikata servisa

   ```bash
   makecert -sv SBESService.pvk -iv ProjekatCA.pvk -n "CN=sbesservice" -pe -ic ProjekatCA.cer SBESService.cer -sr localmachine -ss My -sky exchange
   ```

   Generisanje pfx fajla

   ```bash
   pvk2pfx.exe /pvk SBESService.pvk /pi 123 /spc SBESService.cer /pfx SBESService.pfx
   ```

   Instaliraj oba fajla na Personal lokaciju

3. Generisanje Klijenstskih sertifikata

   Admins

   ```bash
   makecert -sv SBESClientAdmin.pvk -iv ProjekatCA.pvk -n "CN=sbesclientadmin,OU=Admins,O=ProjekatCA" -pe -ic ProjekatCA.cer SBESClientAdmin.cer -sr localmachine -ss My -sky exchange
   ```

   Generisanje pfx fajla

   ```bash
   pvk2pfx.exe /pvk SBESClientAdmin.pvk /pi 123 /spc SBESClientAdmin.cer /pfx SBESClientAdmin.pfx
   ```

   Readers

   ```bash
   makecert -sv SBESClientReader.pvk -iv ProjekatCA.pvk -n "CN=sbesclientreader,OU=Readers,O=ProjekatCA" -pe -ic ProjekatCA.cer SBESClientReader.cer -sr localmachine -ss My -sky exchange
   ```

   pfx

   ```bash
   pvk2pfx.exe /pvk SBESClientReader.pvk /pi 123 /spc SBESClientReader.cer /pfx SBESClientReader.pfx
   ```

   Writers

   ```bash
   makecert -sv SBESClientWriter.pvk -iv ProjekatCA.pvk -n "CN=sbesclientwriter,OU=Writers,O=ProjekatCA" -pe -ic ProjekatCA.cer SBESClientWriter.cer -sr localmachine -ss My -sky exchange
   ```

   pfx

   ```bash
   pvk2pfx.exe /pvk SBESClientWriter.pvk /pi 123 /spc SBESClientWriter.cer /pfx SBESClientWriter.pfx
   ```



Zatim napravi sve user-e:

1. sbesservice
2. sbesclientadmin
3. sbesclientreader
4. sbesclientwriter



I dodaj im pravo pristupa privatnom kljucu svojih sertifikata na sledeci nacin:

![1579187994605](C:\Users\vladimir\AppData\Roaming\Typora\typora-user-images\1579187994605.png)







