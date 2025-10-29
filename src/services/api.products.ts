import { GetProductsParams, Product } from "@/types";

export const productsApi = {
  findMany: (params: GetProductsParams): Product[] => {
    return [
      {
        id: 1,
        categoryId: 1,
        name: "Café",
        description: "(de corte origen brasil)",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Espresso",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Espresso macchiato",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Doppio",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Lungo",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Americano simple/doble",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Cortado",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Flat White",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Capuchino",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Latte",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Moca",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Caramel Latte",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Vanilla latte",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Avellana latte",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Nutella latte",
            description: "Chocolate especial con pasta de avellanas",
          },
        ],
        enabled: true,
      },
      {
        id: 2,
        categoryId: 1,
        name: "Especial del día",
        description: "(consultar origen)",
        price: Number((Math.random() * 1000).toFixed(0)),
        enabled: true,
      },
      {
        id: 3,
        categoryId: 1,
        name: "Frío",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Espresso con hielo",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Espresso tonic",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Cold Flat",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Iced Latte",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Iced caramel latte",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Iced vanilla latte",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Iced avellana latte",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Iced americano simple/doble",
            description: "",
          },
        ],
        enabled: true,
      },
      {
        id: 4,
        categoryId: 2,
        name: "Croissant",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Con jamon cocido y queso tybo",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Con rucula, tomates secos, queso parmesano y oliva",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Con dulce de leche y azucar impalpable",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Con nutella y azucar impalpable",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Croissant",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Pain au chocolat",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Roll de canela",
            description: "",
          },
        ],
        enabled: true,
      },
      {
        id: 5,
        categoryId: 2,
        name: "Budines",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Zanahoria",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Limon y amapolas",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Avellanas y chocolate (vegano)",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Banana y tahini",
            description: "",
          },
        ],
        enabled: true,
      },
      {
        id: 6,
        categoryId: 2,
        name: "Galletas",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Almendras coco y chocolate negro.",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Limón chocolate blanco y pistacho.",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Chocolate con sal (vegana)",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Alfajor de nuez.",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Scones de queso y pimienta",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Scones de queso y verdeo",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Chipa",
            description: "",
          },
        ],
        enabled: true,
      },
      {
        id: 7,
        categoryId: 2,
        name: "Sin TACC",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Alfajorcito de mani con dulce de leche y sésamo",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Alfajorcito de mani con chocolate y dulce de leche",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Alfajorcito de maicena",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Pepas (2 unidades)",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Conito de dulce de leche",
            description: "",
          },
        ],
        enabled: true,
      },
      {
        id: 8,
        categoryId: 3,
        name: "Sandwiches",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Catalan",
            description: "Jamón cocido y queso tybo con pulpa de tomate en pan baguette.",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Mediterraneo",
            description:
              "Rucula, toates cherry confitados, queso tybo, pesto de albahaca, aceitunas negras en pan baguette.",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Vegetariano",
            description:
              "Mix de hojas verdes, cherry confitado, zucchini, queso tybo, pepino, mayo de zanahorias, en pan ciabatta.",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Pauza",
            description:
              "Lomito ahumado, rúcula, pimientossalteados, cebolla caramelizada, parmesano y lactonesa de hierbas en pan ciabatta",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Sandwich Croque Monsieur",
            description: "Jamón cocido, queso tybo en pan de molde con salsa bechamel y queso gratinado.",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Sandwich Croque Madame",
            description: "Jamón cocido, queso tybo en pan de molde con salsa bechamel,queso gratinado y huevo frito.",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Bagel",
            description: "Lomito ahumado, queso tybo y huevo.",
          },
        ],
        enabled: true,
      },
      {
        id: 9,
        categoryId: 3,
        name: "Tostones",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "De pan de molde con mermelada/miel y queso crema/ manteca.",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "De pan de campo con pulpa de tomate, queso parmesano y pesto de albahaca.",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "De pan de campo con queso crema, palta y huevo revuelto.",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "De hogaza maíz morado con queso crema, palta, brotes de lenteja y semillas.",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Francesa con fruta de estación y granola con culis de naranja y menta.",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Yogurt con granola, frutas de estación y miel",
            description: "",
          },
        ],
        enabled: true,
      },
      {
        id: 10,
        categoryId: 4,
        price: Number((Math.random() * 1000).toFixed(0)),
        name: "Completo",
        description:
          "Infusion, tostadas de pan de campo y pan de molde, jamon cocido, queso tybo, huevo revuelto con semillas, queso crema y un bowl con yogurt natural, miel y granola",
        enabled: true,
      },
      {
        id: 11,
        categoryId: 4,
        price: Number((Math.random() * 1000).toFixed(0)),
        name: "Lightweight",
        description:
          "Infusión tostadas de pan de molde, pulpa de tomate y oliva y bowl con yogurt natural, miel, frutas de estación y granola.",
        enabled: true,
      },
      {
        id: 12,
        categoryId: 4,
        price: Number((Math.random() * 1000).toFixed(0)),
        name: "Baby breakfast",
        description: "Infusión,panqueques de avena y banana con miel, frutos secos y frutas de estación",
        enabled: true,
      },
      {
        id: 13,
        categoryId: 5,
        name: "Tartas",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Tarta veggie",
            description:
              "Espuma de espinaca concubos de calabacin y choclo salteado, tomate cherry,cebollacaramelizada en masa quebrada.",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Tarta gourmet",
            description: "Pollo, cebollas caramelizadas, espinaca, queso azul y nueces en masa quebrada.",
          },
        ],
        enabled: true,
      },
      {
        id: 14,
        categoryId: 5,
        name: "Wrap",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Wrap frio",
            description: "Tortilla de trigo , mix de verdes, palta, zanahoria, pollo y pate de remolacha y semillas.",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Wrap calentito",
            description:
              "Tortilla de trigo calentita, pollo, queso tybo, cebolla caramelizada, zanahoria, pimiento rojo y verde, con mostaza Pauza (mostaza, curry y miel).",
          },
        ],
        enabled: true,
      },
      {
        id: 15,
        categoryId: 5,
        name: "Ensaladas",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Cesar",
            description:
              "Lechuga manteca, tomates cherry confitados, jamon cocido y queso tybo en tiras, crutones y pollo al horno con salsa césar",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Pauza",
            description:
              "Mix de verdes, zanahoria, pepino, cherry confitados, crutones, brotes de lenteja, acompañado con vinagreta de yogurt natural",
          },
        ],
        enabled: true,
      },
      {
        id: 16,
        categoryId: 6,
        name: "Naturales",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Limonada menta y jengibre 1/2L.",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Jugo de naranja exprimido",
            description: "",
          },
        ],
        enabled: true,
      },
      {
        id: 17,
        categoryId: 6,
        name: "Otras bebidas",
        variants: [
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Gaseosa lata 330 cc",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Agua mineal 500 cc",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Agua mineral con gas 500 cc",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Stella Artois 330 cc",
            description: "",
          },
          {
            price: Number((Math.random() * 1000).toFixed(0)),
            name: "Sidra Peer",
            description: "",
          },
        ],
        enabled: true,
      },
      {
        id: 18,
        categoryId: 7,
        price: Number((Math.random() * 1000).toFixed(0)),
        name: "Vermút",
        description: "(Aperitivo a base de hierbas y especias acompañado con dash de soda, hielo y rodaja de naranja)",
        variants: [
          { name: "Cinzano Rosso/Bianco" },
          { name: "Lunfa" },
          { name: "Lunfa Verbena" },
          { name: "La Fuerza Rojo/Blanco" },
        ],
        enabled: true,
      },
      {
        id: 19,
        categoryId: 7,
        price: Number((Math.random() * 1000).toFixed(0)),
        name: "Gin tonic",
        description: "(Destilado inglés acompañado con tónica, hielo y rodaja de limón)",
        variants: [{ name: "Brokers" }, { name: "Tanqueray" }],
        enabled: true,
      },
      {
        id: 20,
        categoryId: 7,
        price: Number((Math.random() * 1000).toFixed(0)),
        name: "Aperol Spritz",
        description: "(Aperol, vino espumante extra brut, dash de soda y rodaja de naranja)",
        enabled: true,
      },
      {
        id: 21,
        categoryId: 7,
        price: Number((Math.random() * 1000).toFixed(0)),
        name: "Negroni",
        description: "(Vermút, gin, campari y rodaja de naranja)",
        enabled: true,
      },
    ];
  },
};
