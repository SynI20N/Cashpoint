<h1>Cashpoint imitation using dynamic programming algorithm</h1>
<h2>Prerequisites: ASP.NET Core 8.0</h2>
<p>Compile:<code>dotnet build</code></p>
<p>Run:<code>dotnet run</code></p>
<p>Run with docker: <code>sudo docker compose up -d</code></p>
<p>
  How different choices affect algorithm: <br>
  <img src="https://github.com/SynI20N/Cashpoint/blob/main/img/1.png" height="600" width="600"> <br>
  <img src="https://github.com/SynI20N/Cashpoint/blob/main/img/2.png" height="600" width="600"> <br>
  <img src="https://github.com/SynI20N/Cashpoint/blob/main/img/3.png" height="600" width="600"> <br>
  <img src="https://github.com/SynI20N/Cashpoint/blob/main/img/4.png" height="600" width="600"> <br>
  Tough case with prime numbers: <br>
  <img src="https://github.com/SynI20N/Cashpoint/blob/main/img/5.png" height="600" width="600">
</p>
<h3>Защита от DoS с использованием LeakyBucketMiddleware</h3>

#### Параметры лимитёра

```csharp
public class LeakyBucketOptions
{
    public int BucketSize { get; set; } = 100; // Размер ведра
    public int ProcessingRate { get; set; } = 1; // Скорость обработки запросов в секунду
    public int Timeout { get; set; } = 1000; // Время между проверками в миллисекундах
}
```

#### Механизм работы

1. **Обработка входящих запросов:**
   - Получаем IP-адрес клиента
   - Проверяем наличие данного IP в словаре
   - Пытаемся добавить запрос в ведро
   - Если ведро полное - возвращаем 429 Too Many Requests

2. **Опорожнение ведер:**
   - Таймер срабатывает каждые `_options.Timeout` миллисекунд
   - Для каждого IP уменьшаем количество токенов
   - Скорость опустошения определяется `_options.ProcessingRate`

#### Интеграция в приложение

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<LeakyBucketOptions>(options =>
        {
            options.BucketSize = 15;
            options.ProcessingRate = 10;
            options.Timeout = 1000;
        });

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // ...

        app.UseLeakyBucket();

        // ...
    }
}
```

#### Преимущества решения

1. **Защита от DDoS-атак:**
   - Ограничение количества запросов от одного IP
   - Плавное регулирование нагрузки

2. **Гибкая конфигурация:**
   - Настройка размера ведра
   - Регулировка скорости обработки
   - Настройка интервала проверок

3. **Параллельная обработка:**
   - Использование ConcurrentDictionary
   - Атомарные операции с токенами

4. **Простота реализации и интеграции**

#### Рекомендации по настройке параметров
   - `BucketSize` должен соответствовать ожидаемой нагрузке
   - `ProcessingRate` определяет допустимую скорость запросов
   - `Timeout` влияет на точность ограничения
