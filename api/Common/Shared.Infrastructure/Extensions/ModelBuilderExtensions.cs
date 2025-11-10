using Dawn;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entities;
using Shared.Infrastructure.EntityConfiguration;

namespace Shared.Infrastructure.Extensions;
public static class ModelBuilderExtensions
{
    public static void ConfigureTables(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new ConsumptionConfiguration());
        modelBuilder.ApplyConfiguration(new EstablishmentConfiguration());
        modelBuilder.ApplyConfiguration(new ImageConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientConfiguration());
        modelBuilder.ApplyConfiguration(new LayoutConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new RequestConfiguration());
        modelBuilder.ApplyConfiguration(new TableConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    private static TEntity WithId<TEntity>(this TEntity entity, int id) where TEntity : class
    {
        Guard.Argument(() => id).Positive();

        entity.GetType().GetProperty("Id")?.SetValue(entity, id);

        return entity;
    }

    public static void SeedTables(this ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>().HasData(
            new User(username: "DbSeeder", email: "root@edimus.com", password: "T3s7P@ssw0rd", role: UserRole.Root).WithId(1)
        );

        modelBuilder.Entity<Company>().HasData(
            new Company(name: "Maria Antonieta", "Universo Deli", "MA").WithId(1)
        );

        modelBuilder.Entity<Establishment>().HasData(
            new Establishment(1, "Barrio Jardin").WithId(1)
        );

        modelBuilder.Entity<Layout>().HasData(
            new Layout(1, "Planta Baja").WithId(1)
        );

        modelBuilder.Entity<Table>().HasData(
            new Table(layoutId: 1, positionX: 0, positionY: 0, surface: new(){(0,0)}, status: TableStatus.Occupied).WithId(1),
            new Table(layoutId: 1, positionX: 2, positionY: 2, surface: new(){(2,2)}, status: TableStatus.Free).WithId(2),
            new Table(layoutId: 1, positionX: 7, positionY: 0, surface: new(){(7,0), (7,1)}, status: TableStatus.Free).WithId(3),
            new Table(layoutId: 1, positionX: 9,positionY: 0,surface: new() {(9,0), (9,1)}, status: TableStatus.Occupied).WithId(4),
            new Table(layoutId: 1, positionX: 11, positionY: 0, surface: new() {(11,0)}, status: TableStatus.Calling).WithId(5),
            new Table(layoutId: 1, positionX: 13, positionY: 0, surface: new(){(13,0), (13,1)}, status: TableStatus.Occupied).WithId(6),
            new Table(layoutId: 1, positionX: 15, positionY: 1, surface: new() {(15,1)}, status: TableStatus.Calling).WithId(7)
        );

        modelBuilder.Entity<Wall>().HasData(
            new Wall(
                layoutId: 1,
                type: WallType.Solid,
                surface: new() {
                    (3, 0),(3, 2),(0, 3),(1, 3),(2, 3),(3, 3),(4, 3),(5, 3),(6, 3),
                    (7, 3),(8, 3),(9, 3),(10, 3),(11, 3),(12, 3),(13, 3),(14, 3),
                    (15, 3),(11, 4),(11, 5),(11, 6),(0, 7),(1, 7),(2, 7),(3, 7),
                    (4, 7),(5, 7),(11, 7),(5, 8),(11, 8),(5, 9),(11, 9),(5, 10),
                    (11, 10),(5, 11),(11, 11),(5, 12),(11, 12),(5, 13),(11, 13),
                    (5, 14),(11, 14),(0, 15),(1, 15),(2, 15),(3, 15),(4, 15),
                    (5, 15),(6, 15),(7, 15),(8, 15),(9, 15),(10, 15),(11, 15),
                    (12, 15),(13, 15),(14, 15),(15, 15)
                }
            ).WithId(1),
        new Wall(
            layoutId: 1, 
            type: WallType.Doorway, 
            surface: new() {
                (5, 3),
                (6, 3),
                (5, 9),
                (11, 5),
                (7, 15),
                (3, 15)
            }).WithId(2)
        );

        modelBuilder.Entity<Category>().HasData(
            new Category(companyId: 1, name: "Cafetería").WithId(1),
            new Category(companyId: 1, name: "Pastelería").WithId(2),
            new Category(companyId: 1, name: "Especialidades").WithId(3),
            new Category(companyId: 1, name: "Combos").WithId(4),
            new Category(companyId: 1, name: "Almuerzos").WithId(5),
            new Category(companyId: 1, name: "Bebidas").WithId(6),
            new Category(companyId: 1, name: "Coctelería").WithId(7)
        );

        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient(name: "Harina 000", stock: 50, unit: MeasurementUnit.Kilogram, alert: 10).WithId(1),
            new Ingredient(name: "Tomate fresco", stock: 30, unit: MeasurementUnit.Kilogram, alert: 5).WithId(2),
            new Ingredient(name: "Queso mozzarela", stock: 20, unit: MeasurementUnit.Kilogram, alert: 3).WithId(3),
            new Ingredient(name: "Aceite de oliva", stock: 15, unit: MeasurementUnit.Liter, alert: 2).WithId(4),
            new Ingredient(name: "Cebolla", stock: 3, unit: MeasurementUnit.Kilogram, alert: 4).WithId(5),
            new Ingredient(name: "Pollo", stock: 10, unit: MeasurementUnit.Kilogram, alert: 2).WithId(6),
            new Ingredient(name: "Pasta seca", stock: 40, unit: MeasurementUnit.Kilogram, alert: 8).WithId(7),
            new Ingredient(name: "Salsa de tom ", stock: 12, unit: MeasurementUnit.Liter, alert: 2).WithId(8),
            new Ingredient(name: "Albaca", stock: 200, unit: MeasurementUnit.Gram, alert: 50).WithId(9),
            new Ingredient(name: "Vino tinto", stock: 8, unit: MeasurementUnit.Liter, alert: 1).WithId(10)
        );

        modelBuilder.Entity<Product>().HasData(
            // Café (id: 1)
            new Product(parentId: null, categoryId: 1, name: "Café", price: 0, description: "(de corte origen brasil)").WithId(1),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Espresso").WithId(22),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Espresso macchiato").WithId(23),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Doppio").WithId(24),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Lungo").WithId(25),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Americano simple/doble").WithId(26),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Cortado").WithId(27),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Flat White").WithId(28),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Capuchino").WithId(29),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Latte").WithId(30),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Moca").WithId(31),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Caramel Latte").WithId(32),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Vanilla latte").WithId(33),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Avellana latte").WithId(34),
            new Product(parentId: 1, categoryId: null, price: 1000, name: "Nutella latte", description: "Chocolate especial con pasta de avellanas").WithId(35),
            
            // Especial del día (id: 2)
            new Product(parentId: null, categoryId: 1, price: 1000, name: "Especial del día", description: "(consultar origen)").WithId(2),
            
            // Frío (id: 3)
            new Product(parentId: null, categoryId: 1, price: 0, name: "Frío").WithId(3),
            new Product(parentId: 3, categoryId: null, price: 1000, name: "Espresso con hielo").WithId(36),
            new Product(parentId: 3, categoryId: null, price: 1000, name: "Espresso tonic").WithId(37),
            new Product(parentId: 3, categoryId: null, price: 1000, name: "Cold Flat").WithId(38),
            new Product(parentId: 3, categoryId: null, price: 1000, name: "Iced Latte").WithId(39),
            new Product(parentId: 3, categoryId: null, price: 1000, name: "Iced caramel latte").WithId(40),
            new Product(parentId: 3, categoryId: null, price: 1000, name: "Iced vanilla latte").WithId(41),
            new Product(parentId: 3, categoryId: null, price: 1000, name: "Iced avellana latte").WithId(42),
            new Product(parentId: 3, categoryId: null, price: 1000, name: "Iced americano simple/doble").WithId(43),
            
            // Croissant (id: 4, corregido)
            new Product(parentId: null, categoryId: 2, price: 0, name: "Croissant").WithId(4),
            new Product(parentId: 4, categoryId: null, price: 1000, name: "Con jamon cocido y queso tybo").WithId(44),
            new Product(parentId: 4, categoryId: null, price: 1000, name: "Con rucula, tomates secos, queso parmesano y oliva").WithId(45),
            new Product(parentId: 4, categoryId: null, price: 1000, name: "Con dulce de leche y azucar impalpable").WithId(46),
            new Product(parentId: 4, categoryId: null, price: 1000, name: "Con nutella y azucar impalpable").WithId(47),
            new Product(parentId: 4, categoryId: null, price: 1000, name: "Croissant").WithId(48),
            new Product(parentId: 4, categoryId: null, price: 1000, name: "Pain au chocolat").WithId(49),
            new Product(parentId: 4, categoryId: null, price: 1000, name: "Roll de canela").WithId(50),
            
            // Budines (id: 5)
            new Product(parentId: null, categoryId: 2, price: 0, name: "Budines").WithId(5),
            new Product(parentId: 5, categoryId: null, price: 1000, name: "Zanahoria").WithId(51),
            new Product(parentId: 5, categoryId: null, price: 1000, name: "Limon y amapolas").WithId(52),
            new Product(parentId: 5, categoryId: null, price: 1000, name: "Avellanas y chocolate (vegano)").WithId(53),
            new Product(parentId: 5, categoryId: null, price: 1000, name: "Banana y tahini").WithId(54),
            
            // Galletas (id: 6)
            new Product(parentId: null, categoryId: 2, price: 0, name: "Galletas").WithId(6),
            new Product(parentId: 6, categoryId: null, price: 1000, name: "Almendras coco y chocolate negro.").WithId(55),
            new Product(parentId: 6, categoryId: null, price: 1000, name: "Limón chocolate blanco y pistacho.").WithId(56),
            new Product(parentId: 6, categoryId: null, price: 1000, name: "Chocolate con sal (vegana)").WithId(57),
            new Product(parentId: 6, categoryId: null, price: 1000, name: "Alfajor de nuez.").WithId(58),
            new Product(parentId: 6, categoryId: null, price: 1000, name: "Scones de queso y pimienta").WithId(59),
            new Product(parentId: 6, categoryId: null, price: 1000, name: "Scones de queso y verdeo").WithId(60),
            new Product(parentId: 6, categoryId: null, price: 1000, name: "Chipa").WithId(61),
            
            // Sin TACC (id: 7)
            new Product(parentId: null, categoryId: 2, price: 0, name: "Sin TACC").WithId(7),
            new Product(parentId: 7, categoryId: null, price: 1000, name: "Alfajorcito de mani con dulce de leche y sésamo").WithId(62),
            new Product(parentId: 7, categoryId: null, price: 1000, name: "Alfajorcito de mani con chocolate y dulce de leche").WithId(63),
            new Product(parentId: 7, categoryId: null, price: 1000, name: "Alfajorcito de maicena").WithId(64),
            new Product(parentId: 7, categoryId: null, price: 1000, name: "Pepas (2 unidades)").WithId(65),
            new Product(parentId: 7, categoryId: null, price: 1000, name: "Conito de dulce de leche").WithId(66),
            
            // Sandwiches (id: 8)
            new Product(parentId: null, categoryId: 3, price: 0, name: "Sandwiches").WithId(8),
            new Product(parentId: 8, categoryId: null, price: 1000, name: "Catalan", description: "Jamón cocido y queso tybo con pulpa de tomate en pan baguette.").WithId(67),
            new Product(parentId: 8, categoryId: null, price: 1000, name: "Mediterraneo", description: "Rucula, toates cherry confitados, queso tybo, pesto de albahaca, aceitunas negras en pan baguette.").WithId(68),
            new Product(parentId: 8, categoryId: null, price: 1000, name: "Vegetariano", description: "Mix de hojas verdes, cherry confitado, zucchini, queso tybo, pepino, mayo de zanahorias, en pan ciabatta.").WithId(69),
            new Product(parentId: 8, categoryId: null, price: 1000, name: "Pauza", description: "Lomito ahumado, rúcula, pimientossalteados, cebolla caramelizada, parmesano y lactonesa de hierbas en pan ciabatta").WithId(70),
            new Product(parentId: 8, categoryId: null, price: 1000, name: "Sandwich Croque Monsieur", description: "Jamón cocido, queso tybo en pan de molde con salsa bechamel y queso gratinado.").WithId(71),
            new Product(parentId: 8, categoryId: null, price: 1000, name: "Sandwich Croque Madame", description: "Jamón cocido, queso tybo en pan de molde con salsa bechamel,queso gratinado y huevo frito.").WithId(72),
            new Product(parentId: 8, categoryId: null, price: 1000, name: "Bagel", description: "Lomito ahumado, queso tybo y huevo.").WithId(73),
            
            // Tostones (id: 9)
            new Product(parentId: null, categoryId: 3, price: 0, name: "Tostones").WithId(9),
            new Product(parentId: 9, categoryId: null, price: 1000, name: "De pan de molde con mermelada/miel y queso crema/ manteca.").WithId(74),
            new Product(parentId: 9, categoryId: null, price: 1000, name: "De pan de campo con pulpa de tomate, queso parmesano y pesto de albahaca.").WithId(75),
            new Product(parentId: 9, categoryId: null, price: 1000, name: "De pan de campo con queso crema, palta y huevo revuelto.").WithId(76),
            new Product(parentId: 9, categoryId: null, price: 1000, name: "De hogaza maíz morado con queso crema, palta, brotes de lenteja y semillas.").WithId(77),
            new Product(parentId: 9, categoryId: null, price: 1000, name: "Francesa con fruta de estación y granola con culis de naranja y menta.").WithId(78),
            new Product(parentId: 9, categoryId: null, price: 1000, name: "Yogurt con granola, frutas de estación y miel").WithId(79),
            
            // Completo (id: 10)
            new Product(parentId: null, categoryId: 4, price: 1000, name: "Completo", description: "Infusion, tostadas de pan de campo y pan de molde, jamon cocido, queso tybo, huevo revuelto con semillas, queso crema y un bowl con yogurt natural, miel y granola").WithId(10),
            
            // Lightweight (id: 11)
            new Product(parentId: null, categoryId: 4, price: 1000, name: "Lightweight", description: "Infusión tostadas de pan de molde, pulpa de tomate y oliva y bowl con yogurt natural, miel, frutas de estación y granola.").WithId(11),
            
            // Baby breakfast (id: 12)
            new Product(parentId: null, categoryId: 4, price: 1000, name: "Baby breakfast", description: "Infusión,panqueques de avena y banana con miel, frutos secos y frutas de estación").WithId(12),
            
            // Tartas (id: 13)
            new Product(parentId: null, categoryId: 5, price: 0, name: "Tartas").WithId(13),
            new Product(parentId: 13, categoryId: null, price: 1000, name: "Tarta veggie", description: "Espuma de espinaca concubos de calabacin y choclo salteado, tomate cherry,cebollacaramelizada en masa quebrada.").WithId(80),
            new Product(parentId: 13, categoryId: null, price: 1000, name: "Tarta gourmet", description: "Pollo, cebollas caramelizadas, espinaca, queso azul y nueces en masa quebrada.").WithId(81),
            
            // Wrap (id: 14)
            new Product(parentId: null, categoryId: 5, price: 0, name: "Wrap").WithId(14),
            new Product(parentId: 14, categoryId: null, price: 1000, name: "Wrap frio", description: "Tortilla de trigo , mix de verdes, palta, zanahoria, pollo y pate de remolacha y semillas.").WithId(82),
            new Product(parentId: 14, categoryId: null, price: 1000, name: "Wrap calentito", description: "Tortilla de trigo calentita, pollo, queso tybo, cebolla caramelizada, zanahoria, pimiento rojo y verde, con mostaza Pauza (mostaza, curry y miel).").WithId(83),
            
            // Ensaladas (id: 15)
            new Product(parentId: null, categoryId: 5, price: 0, name: "Ensaladas").WithId(15),
            new Product(parentId: 15, categoryId: null, price: 1000, name: "Cesar", description: "Lechuga manteca, tomates cherry confitados, jamon cocido y queso tybo en tiras, crutones y pollo al horno con salsa césar").WithId(84),
            new Product(parentId: 15, categoryId: null, price: 1000, name: "Pauza", description: "Mix de verdes, zanahoria, pepino, cherry confitados, crutones, brotes de lenteja, acompañado con vinagreta de yogurt natural").WithId(85),
            
            // Naturales (id: 16)
            new Product(parentId: null, categoryId: 6, price: 0, name: "Naturales").WithId(16),
            new Product(parentId: 16, categoryId: null, price: 1000, name: "Limonada menta y jengibre 1/2L.").WithId(86),
            new Product(parentId: 16, categoryId: null, price: 1000, name: "Jugo de naranja exprimido").WithId(87),
            
            // Otras bebidas (id: 17)
            new Product(parentId: null, categoryId: 6, price: 0, name: "Otras bebidas").WithId(17),
            new Product(parentId: 17, categoryId: null, price: 1000, name: "Gaseosa lata 330 cc").WithId(88),
            new Product(parentId: 17, categoryId: null, price: 1000, name: "Agua mineal 500 cc").WithId(89),
            new Product(parentId: 17, categoryId: null, price: 1000, name: "Agua mineral con gas 500 cc").WithId(90),
            new Product(parentId: 17, categoryId: null, price: 1000, name: "Stella Artois 330 cc").WithId(91),
            new Product(parentId: 17, categoryId: null, price: 1000, name: "Sidra Peer").WithId(92),
            
            // Vermút (id: 18)
            new Product(parentId: null, categoryId: 7, price: 1000, name: "Vermút", description: "(Aperitivo a base de hierbas y especias acompañado con dash de soda, hielo y rodaja de naranja)").WithId(18),
            new Product(parentId: 18, categoryId: null, price: 0, name: "Cinzano Rosso/Bianco").WithId(93),
            new Product(parentId: 18, categoryId: null, price: 0, name: "Lunfa").WithId(94),
            new Product(parentId: 18, categoryId: null, price: 0, name: "Lunfa Verbena").WithId(95),
            new Product(parentId: 18, categoryId: null, price: 0, name: "La Fuerza Rojo/Blanco").WithId(96),
            
            // Gin tonic (id: 19)
            new Product(parentId: null, categoryId: 7, price: 1000, name: "Gin tonic", description: "(Destilado inglés acompañado con tónica, hielo y rodaja de limón)").WithId(19),
            new Product(parentId: 19, categoryId: null, price: 0, name: "Brokers").WithId(97),
            new Product(parentId: 19, categoryId: null, price: 0, name: "Tanqueray").WithId(98),
            
            // Aperol Spritz (id: 20)
            new Product(parentId: null, categoryId: 7, price: 1000, name: "Aperol Spritz", description: "(Aperol, vino espumante extra brut, dash de soda y rodaja de naranja)").WithId(20),
            
            // Negroni (id: 21)
            new Product(parentId: null, categoryId: 7, price: 1000, name: "Negroni", description: "(Vermút, gin, campari y rodaja de naranja)").WithId(21)
        );
    }
}

