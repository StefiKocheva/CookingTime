namespace CookingTime.Data;

using CookingTime.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Security.Claims;

public class CookingTimeDbContext : IdentityDbContext<User>
{
    public CookingTimeDbContext(DbContextOptions<CookingTimeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Favorite> Favorites { get; set; }

    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Recipe>()
            .HasOne(r => r.User)
            .WithMany(u => u.Recipes)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(c => c.Recipe)
            .WithMany(r => r.Comments)
            .HasForeignKey(c => c.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Favorite>()
            .HasKey(f => new { f.RecipeId, f.UserId });

        builder.Entity<Favorite>()
            .HasOne(f => f.Recipe)
            .WithMany(r => r.Favorites)
            .HasForeignKey(f => f.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Favorite>()
            .HasOne(f => f.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        var roleId = Guid.NewGuid().ToString();

        builder.Entity<IdentityRole>()
            .HasData(new IdentityRole()
            {
                Id = roleId,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = roleId
            });

        var adminId = Guid.NewGuid().ToString();

        var admin = new User()
        {
            Id = adminId,
            Email = "admin@cookingtime.com",
            NormalizedEmail = "ADMIN@COOKINGTIME.COM",
            EmailConfirmed = true,
            UserName = "admin@cookingtime.com",
            NormalizedUserName = "ADMIN@COOKINGTIME.COM",
            FirstName = "Петър",
            LastName = "Стоянов",
            ImageUrl = @"\img\uploads\profiles\default-image.jpg"
        };

        var ph = new PasswordHasher<User>();
        admin.PasswordHash = ph.HashPassword(admin, "Admin_CookingTime_2024");

        builder.Entity<User>()
            .HasData(admin);

        builder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string>()
            {
                UserId = adminId,
                RoleId = roleId
            });

        var userId = Guid.NewGuid().ToString();
        var user = new User
        {
            Id = userId,
            FirstName = "Ивана",
            LastName = "Петрова",
            Bio = "Приветствайте гурмето в мен! Аз съм готвач със сърце и страст. Обичам да създавам кулинарни шедьоври, като моят специалитет е италиански пастичио с таен сос от баба. Също така, в профила ми ще откриете любими рецепти за сладкиши. С години опит в кухнята, аз не просто готвя, а създавам вкусове и моменти. Вдъхновете се от моите рецепти и дайте на вкусовете да ви отведат на пътешествие! \ud83c\udf5d\ud83d\udd2a",
            Email = "user@cookingtime.com",
            NormalizedEmail = "USER@COOKINGTIME.COM",
            EmailConfirmed = true,
            UserName = "user@cookingtime.com",
            NormalizedUserName = "USER@COOKINGTIME.COM",
            ImageUrl = @"\img\uploads\profiles\ivana.jpg"
        };

        user.PasswordHash = ph.HashPassword(user, "User_CookingTime_2024");

        builder.Entity<User>()
            .HasData(user);

        builder.Entity<Recipe>()
            .HasData(
                new Recipe
                {
                    Id = 1,
                    Title = "Свежа торта с ягоди",
                    ShortDescription = "Потопете се във вълшебството на това сладко изкушение, което събира в себе си свежестта на сочните ягодите и кадифената текстура на крема сирене. Вкусово пътешествие, което ще ви омагьосва с всяка хапка!",
                    Content = @"Първата стъпка от рецептата за свежа ягодова торта е подготовката на продуктите за блата. Пресявате брашното, заедно с бакпулвера, содата и солта. С помощта на миксер разбивате яйцата, заедно със захарта. Добавете ванилиите, олиото и киселото мляко и отново разбийте. Добавете ягодово пюре и ягодовото сладко. Малко по малко при непрекъснато бъркане добавете сухите съставки и разбийте до хомогенна смес.
                        В правоъгълна форма за печене поставете хартия за печене, изсипете сместа и печете в предварително загрята на 180 градуса фурна за около 30 минути. Блатът е изпечен, когато бучнейки го с клечка за зъби, тя излезе суха. Извадете блата от фурната и го оставете да изстине напълно.
                        В купа разбийте крема сиренето, след което към него добавете пресята пудра захар, а след това добавете и заквасената сметана. Разбийте до хомогенна смес. Разпределете крема върху блата, когато вече е съвсем изстинал. Украсете с нарязаните ягоди.",
                    Ingredients =
                        "4 броя яйца, 1/2 чаена чаша сладко, 500 грама крема сирене, 1/3 чаена чаша пудра захар, 1 чаена чаша ягоди, 1/2 броя лайм / зелен лимон",
                    Portions = 16,
                    PreparationTime = 1,
                    Type = RecipeType.Dessert,
                    ImageUrl = @"\img\uploads\recipes\cake.jpg",
                    UserId = userId
                },
                new Recipe
                {
                    Id = 2,
                    Title = "Свински джолан в гювеч",
                    ShortDescription = "Открийте магията на гювеча с тази рецепта за сочен свински джолан, обогатен с нежността на мед и краве масло. Насладете се на този вълшебен опит и позволете на вкусовете да ви отведат на пътешествие из вкусовата галактика!",
                    Content = @"Първата стъпка от рецептата  за свински джолан в гювеч е изборът и подготовката на джолана. Избирате хубав, не много голям свински джолан, като най-добре е да посетите близкия месарски магазин, а да не залагате на джолан от витрината на големите вериги. Измивате добре джолана, добсушавате го, след което го обтривате старателно от всички страни със сол.
                        В гювеч слагате водата и поставяте след това джолана. Следвайте тази последователност на поставяне на съставките в гювеча, защото ако залеете джолана с течноститта, то ще отмиете солта. Слагате гювеча (винаги!) в студена фурна, за да не се пукне. Първаночално включвате на 200 градуса, като след около 30 минути намалявате на 160 градуса и забравяте за джолана за около 4 часа, като на всеки 40-50 минути изваждате, отваряте и обливате печащия се джолан със соса.
                        След около 4 часа във фурната, изваждате джолана и в малка купичка разбърквате кравето масло, меда и 1-2 супени лъжици от соса. С тази смес намазвате джолана и оставяте за още около 20-30 минути във фурната.
                        Гювечът е един вълшебен съд! Той има способността да превръща и най-простичките съставки в истински деликатеси! 
                        ДОБЪР АПЕТИТ!",
                    Ingredients = "1 брой свински джолан, 2 супени лъжици мед, 50 грама краве масло",
                    Portions = 8,
                    PreparationTime = 6.5,
                    Type = RecipeType.MainDish,
                    ImageUrl = @"\img\uploads\recipes\pork.jpg",
                    UserId = userId
                });

        builder.Entity<Favorite>()
            .HasData(
                new Favorite
                {
                    UserId = userId,
                    RecipeId = 1
                },
                new Favorite
                {
                    UserId = userId,
                    RecipeId = 2
                },
                new Favorite
                {
                    UserId = adminId,
                    RecipeId = 1
                },
                new Favorite
                {
                    UserId = adminId,
                    RecipeId = 2
                });

        builder.Entity<Comment>()
            .HasData(
                new Comment
                {
                    Id = 1,
                    Content = "Тази торта е просто възхитителна! Искрени благодарности и поздравления. Рецептата е лесна за приготвяне и резултатът е впечатляващ. Сигурен съм, че всеки, който я опита, ще се влюби в нея!",
                    CreatedOn = DateTime.Now.AddDays(-3),
                    RecipeId = 1,
                    UserId = userId
                },
                new Comment
                {
                    Id = 2,
                    Content = "Абсолютно вълшебна торта! Препоръчвам я на всеки, който обича ягодите и сладките изкушения. Изключително лесна за приготвяне и резултатът е просто невероятен. Гарантирано удоволствие за сетивата и радост за всеки вкус!",
                    CreatedOn = DateTime.Now.AddDays(-5),
                    RecipeId = 1,
                    UserId = adminId
                },
                new Comment
                {
                    Id = 3,
                    Content = "За съжаление, джоланът ми се получи сух и недопечен, въпреки че следвах всички стъпки от рецептата... Не одобрявам!",
                    CreatedOn = DateTime.Now.AddDays(-1),
                    RecipeId = 2,
                    UserId = userId
                },
                new Comment
                {
                    Id = 4,
                    Content = "Тази рецепта за свински джолан в гювеч беше чудесна! Комбинацията от мед и масло придаде изключителен вкус на ястието. Готвенето изискваше време, но резултатът си струваше всеки момент. Определено препоръчвам!",
                    CreatedOn = DateTime.Now.AddDays(-7),
                    RecipeId = 2,
                    UserId = adminId
                });
    }
}
