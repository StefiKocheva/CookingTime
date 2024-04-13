namespace CookingTime.Data.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AspNetRoles",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUsers",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserLogins",
            columns: table => new
            {
                LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserRoles",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserTokens",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Recipes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PreparationTime = table.Column<double>(type: "float", nullable: false),
                Portions = table.Column<double>(type: "float", nullable: false),
                Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Recipes", x => x.Id);
                table.ForeignKey(
                    name: "FK_Recipes_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Comments",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                RecipeId = table.Column<int>(type: "int", nullable: false),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Comments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Comments_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Comments_Recipes_RecipeId",
                    column: x => x.RecipeId,
                    principalTable: "Recipes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Favorites",
            columns: table => new
            {
                RecipeId = table.Column<int>(type: "int", nullable: false),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Favorites", x => new { x.RecipeId, x.UserId });
                table.ForeignKey(
                    name: "FK_Favorites_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Favorites_Recipes_RecipeId",
                    column: x => x.RecipeId,
                    principalTable: "Recipes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            values: new object[] { "c2a7c3f4-31c5-4df1-be95-825cf5aef17a", "c2a7c3f4-31c5-4df1-be95-825cf5aef17a", "Administrator", "ADMINISTRATOR" });

        migrationBuilder.InsertData(
            table: "AspNetUsers",
            columns: new[] { "Id", "AccessFailedCount", "Bio", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImageUrl", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
            values: new object[,]
            {
                { "1389cefe-47bb-4045-bab1-9beee3459af6", 0, null, "cf36ca41-8ab2-483a-b6ee-92ad04fc74c4", "admin@cookingtime.com", true, "Петър", "\\img\\uploads\\profiles\\default-image.jpg", "Стоянов", false, null, "ADMIN@COOKINGTIME.COM", "ADMIN@COOKINGTIME.COM", "AQAAAAIAAYagAAAAEFbuZ9x9P1zvj1SsETlLHJst3dsON3Eh1FVd6XB/YMg0eRfvSViV3eNaocdRaVTWIA==", null, false, "a2e81817-6574-4649-a415-33e0f0785a21", false, "admin@cookingtime.com" },
                { "67b9512c-16b9-4293-aa73-7d4206203c66", 0, "Приветствайте гурмето в мен! Аз съм готвач със сърце и страст. Обичам да създавам кулинарни шедьоври, като моят специалитет е италиански пастичио с таен сос от баба. Също така, в профила ми ще откриете любими рецепти за сладкиши. С години опит в кухнята, аз не просто готвя, а създавам вкусове и моменти. Вдъхновете се от моите рецепти и дайте на вкусовете да ви отведат на пътешествие! 🍝🔪", "1a6865f2-5149-4ae3-b5da-0b0591302881", "user@cookingtime.com", true, "Ивана", "\\img\\uploads\\profiles\\ivana.jpg", "Петрова", false, null, "USER@COOKINGTIME.COM", "USER@COOKINGTIME.COM", "AQAAAAIAAYagAAAAEJUFEGuTaGinU3YIt16AlQGs2NNSglcwnQsVTMIoaFSRfMMYytn6qPvt2CFUHe4X9w==", null, false, "fc08febc-f760-4c8c-9816-27d35604a437", false, "user@cookingtime.com" }
            });

        migrationBuilder.InsertData(
            table: "AspNetUserRoles",
            columns: new[] { "RoleId", "UserId" },
            values: new object[] { "c2a7c3f4-31c5-4df1-be95-825cf5aef17a", "1389cefe-47bb-4045-bab1-9beee3459af6" });

        migrationBuilder.InsertData(
            table: "Recipes",
            columns: new[] { "Id", "Content", "CreatedOn", "ImageUrl", "Ingredients", "Portions", "PreparationTime", "ShortDescription", "Title", "Type", "UserId" },
            values: new object[,]
            {
                { 1, "Първата стъпка от рецептата за свежа ягодова торта е подготовката на продуктите за блата. Пресявате брашното, заедно с бакпулвера, содата и солта. С помощта на миксер разбивате яйцата, заедно със захарта. Добавете ванилиите, олиото и киселото мляко и отново разбийте. Добавете ягодово пюре и ягодовото сладко. Малко по малко при непрекъснато бъркане добавете сухите съставки и разбийте до хомогенна смес.\n                        В правоъгълна форма за печене поставете хартия за печене, изсипете сместа и печете в предварително загрята на 180 градуса фурна за около 30 минути. Блатът е изпечен, когато бучнейки го с клечка за зъби, тя излезе суха. Извадете блата от фурната и го оставете да изстине напълно.\n                        В купа разбийте крема сиренето, след което към него добавете пресята пудра захар, а след това добавете и заквасената сметана. Разбийте до хомогенна смес. Разпределете крема върху блата, когато вече е съвсем изстинал. Украсете с нарязаните ягоди.", new DateTime(2024, 4, 13, 19, 19, 20, 722, DateTimeKind.Local).AddTicks(6585), "\\img\\uploads\\recipes\\cake.jpg", "4 броя яйца, 1/2 чаена чаша сладко, 500 грама крема сирене, 1/3 чаена чаша пудра захар, 1 чаена чаша ягоди, 1/2 броя лайм / зелен лимон", 16.0, 1.0, "Потопете се във вълшебството на това сладко изкушение, което събира в себе си свежестта на сочните ягодите и кадифената текстура на крема сирене. Вкусово пътешествие, което ще ви омагьосва с всяка хапка!", "Свежа торта с ягоди", 4, "67b9512c-16b9-4293-aa73-7d4206203c66" },
                { 2, "Първата стъпка от рецептата  за свински джолан в гювеч е изборът и подготовката на джолана. Избирате хубав, не много голям свински джолан, като най-добре е да посетите близкия месарски магазин, а да не залагате на джолан от витрината на големите вериги. Измивате добре джолана, добсушавате го, след което го обтривате старателно от всички страни със сол.\n                        В гювеч слагате водата и поставяте след това джолана. Следвайте тази последователност на поставяне на съставките в гювеча, защото ако залеете джолана с течноститта, то ще отмиете солта. Слагате гювеча (винаги!) в студена фурна, за да не се пукне. Първаночално включвате на 200 градуса, като след около 30 минути намалявате на 160 градуса и забравяте за джолана за около 4 часа, като на всеки 40-50 минути изваждате, отваряте и обливате печащия се джолан със соса.\n                        След около 4 часа във фурната, изваждате джолана и в малка купичка разбърквате кравето масло, меда и 1-2 супени лъжици от соса. С тази смес намазвате джолана и оставяте за още около 20-30 минути във фурната.\n                        Гювечът е един вълшебен съд! Той има способността да превръща и най-простичките съставки в истински деликатеси! \n                        ДОБЪР АПЕТИТ!", new DateTime(2024, 4, 13, 19, 19, 20, 722, DateTimeKind.Local).AddTicks(6812), "\\img\\uploads\\recipes\\pork.jpg", "1 брой свински джолан, 2 супени лъжици мед, 50 грама краве масло", 8.0, 6.5, "Открийте магията на гювеча с тази рецепта за сочен свински джолан, обогатен с нежността на мед и краве масло. Насладете се на този вълшебен опит и позволете на вкусовете да ви отведат на пътешествие из вкусовата галактика!", "Свински джолан в гювеч", 3, "67b9512c-16b9-4293-aa73-7d4206203c66" }
            });

        migrationBuilder.InsertData(
            table: "Comments",
            columns: new[] { "Id", "Content", "CreatedOn", "RecipeId", "UserId" },
            values: new object[,]
            {
                { 1, "Тази торта е просто възхитителна! Искрени благодарности и поздравления. Рецептата е лесна за приготвяне и резултатът е впечатляващ. Сигурен съм, че всеки, който я опита, ще се влюби в нея!", new DateTime(2024, 4, 10, 19, 19, 20, 722, DateTimeKind.Local).AddTicks(6939), 1, "67b9512c-16b9-4293-aa73-7d4206203c66" },
                { 2, "Абсолютно вълшебна торта! Препоръчвам я на всеки, който обича ягодите и сладките изкушения. Изключително лесна за приготвяне и резултатът е просто невероятен. Гарантирано удоволствие за сетивата и радост за всеки вкус!", new DateTime(2024, 4, 8, 19, 19, 20, 722, DateTimeKind.Local).AddTicks(6953), 1, "1389cefe-47bb-4045-bab1-9beee3459af6" },
                { 3, "За съжаление, джоланът ми се получи сух и недопечен, въпреки че следвах всички стъпки от рецептата... Не одобрявам!", new DateTime(2024, 4, 12, 19, 19, 20, 722, DateTimeKind.Local).AddTicks(6964), 2, "67b9512c-16b9-4293-aa73-7d4206203c66" },
                { 4, "Тази рецепта за свински джолан в гювеч беше чудесна! Комбинацията от мед и масло придаде изключителен вкус на ястието. Готвенето изискваше време, но резултатът си струваше всеки момент. Определено препоръчвам!", new DateTime(2024, 4, 6, 19, 19, 20, 722, DateTimeKind.Local).AddTicks(6969), 2, "1389cefe-47bb-4045-bab1-9beee3459af6" }
            });

        migrationBuilder.InsertData(
            table: "Favorites",
            columns: new[] { "RecipeId", "UserId" },
            values: new object[,]
            {
                { 1, "1389cefe-47bb-4045-bab1-9beee3459af6" },
                { 1, "67b9512c-16b9-4293-aa73-7d4206203c66" },
                { 2, "1389cefe-47bb-4045-bab1-9beee3459af6" },
                { 2, "67b9512c-16b9-4293-aa73-7d4206203c66" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_AspNetRoleClaims_RoleId",
            table: "AspNetRoleClaims",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AspNetRoles",
            column: "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserClaims_UserId",
            table: "AspNetUserClaims",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserLogins_UserId",
            table: "AspNetUserLogins",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserRoles_RoleId",
            table: "AspNetUserRoles",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "AspNetUsers",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "AspNetUsers",
            column: "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_Comments_RecipeId",
            table: "Comments",
            column: "RecipeId");

        migrationBuilder.CreateIndex(
            name: "IX_Comments_UserId",
            table: "Comments",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Favorites_UserId",
            table: "Favorites",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Recipes_UserId",
            table: "Recipes",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AspNetRoleClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserLogins");

        migrationBuilder.DropTable(
            name: "AspNetUserRoles");

        migrationBuilder.DropTable(
            name: "AspNetUserTokens");

        migrationBuilder.DropTable(
            name: "Comments");

        migrationBuilder.DropTable(
            name: "Favorites");

        migrationBuilder.DropTable(
            name: "AspNetRoles");

        migrationBuilder.DropTable(
            name: "Recipes");

        migrationBuilder.DropTable(
            name: "AspNetUsers");
    }
}