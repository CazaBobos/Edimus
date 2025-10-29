import { FlatCompat } from "@eslint/eslintrc";
import { dirname } from "path";
import { fileURLToPath } from "url";

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

const compat = new FlatCompat({
  baseDirectory: __dirname,
});

const eslintConfig = [
  ...compat.extends(),
  ...compat.config({
    extends: [
      "next/core-web-vitals",
      "plugin:@typescript-eslint/recommended",
      "plugin:prettier/recommended",
      "next/typescript",
      "next",
      "prettier",
    ],
    plugins: ["@typescript-eslint", "eslint-plugin-import-helpers"],
    parser: "@typescript-eslint/parser",
    parserOptions: {
      ecmaVersion: "latest",
      sourceType: "module",
    },
    rules: {
      "import-helpers/order-imports": [
        "warn",
        {
          newlinesBetween: "always",
          groups: [["/^next/", "module"], "/^@/styles/", "/^@/components/", "/^@/lib/", ["parent", "sibling", "index"]],
          alphabetize: {
            order: "asc",
            ignoreCase: true,
          },
        },
      ],
    },
  }),
  {
    ignores: ["node_modules/**", ".next/**", "out/**", "build/**", "next-env.d.ts"],
  },
];

export default eslintConfig;
