import { defineConfig } from 'eslint-define-config';
import eslintPluginPrettier from 'eslint-plugin-prettier';
import eslintPluginVue from 'eslint-plugin-vue';

export default defineConfig([
  {
    plugins: {
      prettier: eslintPluginPrettier,
      vue: eslintPluginVue,
    },
    rules: {
      'vue/no-unused-vars': 'warn',
      'vue/max-attributes-per-line': ['error', { singleline: 3 }],
      'vue/require-default-prop': 'warn',
      'prettier/prettier': [
        'error',
        {
          endOfLine: 'auto',
        },
      ],
      'no-console': 'warn',
      'no-debugger': 'warn',
    },
  },
]);
