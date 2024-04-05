<p align="center">
  <img src="https://media.giphy.com/media/Y4ak9Ki2GZCbJxAnJD/giphy.gif" width="150px">
</p>

<p align="center">
  <samp>
    Merhaba, ben Selçuk! 👋<br>
  </samp>
</p>

<p align="center">
  <a href="https://discord.com/users/481831692399673375" target="_blank">
    <img src="https://lanyard.cnrad.dev/api/481831692399673375?hideActivity=true" alt="Discord Presence" style="max-width: 100%;">
  </a>
</p>

<p align="center">
  <a href="https://discordapp.com/users/481831692399673375">
    <img src="https://img.shields.io/badge/Discord-Zyix%231002-7289DA?logo=discord&style=flat-square" alt="Discord">
  </a>
  <a href="https://www.youtube.com/channel/UC7uBi3y2HOCLde5MYWECynQ?view_as=subscriber">
    <img src="https://img.shields.io/badge/YouTube-Subscribe-red?logo=youtube&style=flat-square" alt="YouTube">
  </a>
  <a href="https://www.reddit.com/user/_Zyix">
    <img src="https://img.shields.io/badge/Reddit-Profile-orange?logo=reddit&style=flat-square" alt="Reddit">
  </a>
  <a href="https://open.spotify.com/user/07288iyoa19459y599jutdex6">
    <img src="https://img.shields.io/badge/Spotify-Follow-green?logo=spotify&style=flat-square" alt="Spotify">
  </a>
</p>

<h3 align="center">Kullandığım, bildiğim bazı diller 🏫</h3>
<p align="center">
  <img src="https://img.shields.io/badge/C-00599C?logo=c&logoColor=white&style=flat-square" alt="C">
  <img src="https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=black&style=flat-square" alt="JavaScript">
  <img src="https://img.shields.io/badge/Python-3776AB?logo=python&logoColor=white&style=flat-square" alt="Python">
  <img src="https://img.shields.io/badge/HTML-239120?logo=html5&logoColor=white&style=flat-square" alt="HTML">
  <img src="https://img.shields.io/badge/CSS-239120?logo=css3&logoColor=white&style=flat-square" alt="CSS">
  <img src="https://img.shields.io/badge/Node.js-339933?logo=node.js&logoColor=white&style=flat-square" alt="Node.js">
</p>

<p align="center">
  <samp>
    Parental Computer Control nasıl çalışır?
</samp>

Parental Computer Control çocukların bilgisayar kullanımını yönetmek için tasarlanmış bir C# konsol uygulamasıdır. İki ana yöntem sunar: TimeIntervalControl ve TotalWorkingTimeControl.

## TimeIntervalControl

Bu yöntem, çocukların gün içerisinde belirli zaman aralıklarında bilgisayar kullanımlarının kontrol edilmesini sağlar. Kullanıcılar bilgisayar kullanımına izin verilen başlangıç ve bitiş saatlerini tanımlayabilir. Bu saatler dışında bilgisayar otomatik olarak kapanır.
Örneğin 08:00 ila 17:00 arasında sadece bilgisayar kullanılabilir gibi.

## TotalWorkingTimeControl
Bu yöntem, çocukların her gün bilgisayarda geçirebileceği toplam süreyi kontrol eder. Kullanıcılar çocuğun günlük toplam çalışma saatlerini ayarlayabilir. Belirtilen süre sınırına ulaşıldığında bilgisayar otomatik olarak kapanır.
Örneğin bir gün içersinde 24 saat mevcuttur ve çocuklar bu 24 saat içinde sadece 5 saat bilgisayar kullanabilir.

## Kullanım
- Uygulama debug edildikten sonra ilk açılışta kullanıcıdan şifre isteyecektir bu şifre ileride değiştirilmez, silinmez.
- Girilen şifre onay verdikten sonra 2 seçenekten birisini seçmeniz gerekmektedir.
- Seçeneklerden birisini seçtikten sonra uygulama kendini gizler ve arka planda çalışma işlemine devam eder.
- Uygulama süresi doldu, aştıysa bilgisayar kapatılır bilgisayar tekrar açıldığında kullanıcıya 30 saniye limit verir bu 30 saniye içersinde belirlenen şifreyi girerse uygulama yönetimini değiştirebilir. Eğer girmezse uygulama kaldığı yerden devam edecektir.
- Eğer kullanıcı toplam çalışma saati veya zaman aralığını değiştirmek isterse programın kurulu olduğu dosya dizinde settings.json dosyası mevcuttur bu dosya üzerinden değişiklilik yapılabilir lakin dosya içeriğinde şifre alanının doğru şekilde girilmesi gerekiyor yoksa yapılan değişiklilik geçerli sayılmaz ve eski ayarlarına otomatik döndürür.

## Özelleştirme
Program, zaman aralıkları, toplam çalışma saati gibi çeşitli ayarların özelleştirilebilmesine olanak sağlar. Kullanıcılar bu ayarları kendi özel ihtiyaçlarına uyacak şekilde değiştirebilirler.
</p>
