/* To ćwiczenie stanowi wstęp do bardziej złożonego ćwiczenia - małej aplikacji webowej BeFit.

Na podstawie przykładowego projektu oraz zdobytej wiedzy, stwórz nowy projekt i nazwij go BeFit. W tym celu wykorzystaj szablon ASP.NET Core MVC. UWAGA! Jako typ uwierzytelnienia wybierz "Pojedyncze konta"!

Tak utworzony szablon aplikacji zawiera już gotowy kontekst bazy danych i podstawowy system użytkowników. Korzystając z przykładowego kodu z HomeFinances, ustaw w Program.cs, aby projekt łączył się z bazą danych SQLite. Należy w tym celu zmodyfikować pliki Program.cs i appsettings.json. Należy również usunąć folder Migrations (prawdopodobnie znajduje się w folderze Data).

Następnie zdefiniuj trzy modele. Jeden model ma opisywać typy ćwiczeń jakie można wykonywać na siłowni. Jedynym parametrem tego modelu jest jego nazwa (i oczywiście Id). Ustaw wybrane przez siebie ograniczenie długości nazwy.

Drugi model zawiera informację o sesji treningowej użytkownika. Chwilowo nie będziemy go łączyć z użytkownikiem, ale miejmy to z tyłu głowy. Dwie ważne informacje, które zawiera ten model, to data i czas rozpoczęcia treningu oraz data i czas zakończenia treningu. Jeśli potrafisz możesz spróbować zdefiniować w modelu walidator weryfikujący, czy data rozpoczęcia nie jest późniejsza niż zakończenia. Nie jest to jednak obowiązkowe, bo wymaga własnej definicji atrybutu.

Trzeci model łączy powyższe dwa. Model ten informuje, jaki typ ćwiczenia został wykonany w jakiej sesji treningowej przez jakiego użytkownika (to ostatnie chwilowo pomiń). Ponadto umieść w nim informacje o zastosowanym obciążeniu, liczbie serii i liczbie powtórzeń w serii.

Te trzy modele zarejestruj w kontekście bazy danych i przeprowadź migrację (stwórz i wykonaj). Przy pomocy oprogramowania do analizy plików baz danych sqlite, podejrzyj i przeanalizuj stworzoną strukturę bazy. */

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
