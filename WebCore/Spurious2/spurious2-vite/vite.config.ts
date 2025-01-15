import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5000,
    origin: "http://localhost:5000",
  },
  base: "/client",
  build: {
    outDir: "../wwwroot/client",
    emptyOutDir: true,
  },
  resolve: {
    alias: {
      src: "/src",
      components: "/src/components",
      services: "/src/services",
      pages: "/src/pages",
    },
  },
});
