# سامانه هوشمند مدیریت شرح وظایف سازمانی

این مخزن شامل اسکلت‌بندی کامل یک راهکار سازمانی مبتنی بر **.NET 8** و **Blazor Server** است که مدیریت شرح وظایف اشخاص و واحدها، نسخه‌بندی مستندات و چت سازمانی هوشمند را در قالب معماری Clean پوشش می‌دهد.

## ساختار راه‌حل

```
AiJobEx1.sln
├─ src/
│  ├─ Domain/           # موجودیت‌ها، Value Object ها و رویدادهای دامنه
│  ├─ Application/      # سرویس‌ها، واسط‌ها، الگوهای CQRS (MediatR)
│  ├─ Infrastructure/   # EF Core، Providerهای هوش مصنوعی، سرویس لاگ و پیکربندی
│  ├─ Shared/           # مدل‌های مشترک مانند Result و PagedResult
│  └─ Web/              # پروژه Blazor Server + API، SignalR Hub و UI RTL
└─ tests/
   ├─ UnitTests/        # تست‌های واحد (xUnit)
   └─ IntegrationTests/ # تست‌های یکپارچگی با WebApplicationFactory
```

## قابلیت‌های کلیدی

- مدل دامنه غنی مطابق نیازمندی‌های پروژه (کاربران، واحدها، شرح وظایف، مستندات، لاگ‌ها و ...).
- لایه Application با دستورات/کوئری‌های نمونه برای مدیریت شرح وظایف و تعامل با چت‌بات.
- زیرساخت EF Core با پیکربندی موجودیت‌ها، سرویس ثبت وقایع و Provider ساختگی هوش مصنوعی برای توسعه و تست.
- پروژه Blazor Server با صفحات نمونه (داشبورد، چت، مدیریت کاربر، مستندات) و SignalR Hub برای چت زنده.
- تنظیمات آماده برای Swagger، Health Checks، Serilog و چند زبانه‌سازی (fa-IR و en-US).
- تست‌های واحد و یکپارچگی اولیه برای اطمینان از صحت منطق حیاتی.

## شروع به کار

1. **بازیابی وابستگی‌ها**
   ```bash
   dotnet restore
   ```
2. **اجرای مهاجرت‌ها** (پس از ایجاد Migration)
   ```bash
   dotnet ef database update --project src/Infrastructure --startup-project src/Web
   ```
3. **اجرای برنامه**
   ```bash
   dotnet run --project src/Web
   ```

> **نکته:** در محیط توسعه می‌توانید Provider هوش مصنوعی را با نمونه‌های واقعی (OpenAI/Azure) جایگزین کنید. ساختار DI در `Infrastructure/DependencyInjection.cs` پیش‌بینی شده است.

## تست

برای اجرای تست‌ها:

```bash
dotnet test
```

## برنامه توسعه

- تکمیل Identity و سیاست‌های مجوز بر اساس نقش‌های Admin، HR، Manager و Employee.
- پیاده‌سازی کامل فرآیند RAG با اتصال به منابع برداری/جستجوی متنی.
- افزودن کامپوننت‌های UI پیشرفته (DataGrid، DiffViewer، Uploader) و بهینه‌سازی تجربه کاربری RTL.
- تعبیه گزارش‌گیری پیشرفته (هزینه‌های AI، تعامل کاربران، تغییرات شرح وظایف).
- آماده‌سازی زیرساخت DevOps (Docker, CI/CD, مستندسازی استقرار).
