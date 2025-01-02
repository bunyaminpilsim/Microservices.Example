# Microservices.Example

Bu proje, mikroservis mimarisi üzerine geliştirilmiş bir uygulama örneğidir. Her bir servis bağımsız bir şekilde geliştirilmiş ve farklı işlevler sunmaktadır.

## Proje Yapısı

### Hizmetler (Services)
Proje, aşağıdaki mikroservislerden oluşmaktadır:

- **Product Service:** Ürünlerle ilgili işlemleri yönetir.
- **Order Service:** Siparişlerle ilgili işlemleri yönetir.
- **Stock Service:** Stok yönetimi için geliştirilmiştir.

Her bir mikroservis, kendi sorumluluk alanındaki işlemleri yerine getirmek üzere tasarlanmıştır.

### Teknolojiler
Bu projede kullanılan başlıca teknolojiler şunlardır:

- **Programlama Dili:** C#
- **Framework:** ASP.NET Core
- **Veri Tabanı:** MongoDB
- **Mesajlaşma Sistemi:** RabbitMQ (varsa)

### API İletişimi
Mikroservisler arasındaki iletişim için HTTP protokolü ve RabbitMQ gibi bir mesajlaşma aracı kullanılabilir.

## Kurulum ve Çalıştırma

Aşağıdaki adımları izleyerek projeyi çalıştırabilirsiniz:

### Gereksinimler

- .NET SDK 7.0 veya üzeri
- MongoDB
- RabbitMQ (isteğe bağlı, projede mevcutsa)

