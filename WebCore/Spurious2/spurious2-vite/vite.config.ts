import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import fs from "node:fs";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: parseInt(process.env.HTTPS_PORT as string),
    origin: "https://localhost:" + parseInt(process.env.HTTPS_REDIRECT_PORT as string),
    https: {
      cert: process.env["HTTPS_CERT_FILE"] ? fs.readFileSync(process.env["HTTPS_CERT_FILE"] as string) : "",
      key: process.env["HTTPS_CERT_KEY_FILE"] ? fs.readFileSync(process.env["HTTPS_CERT_KEY_FILE"] as string) : "",
    },
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
