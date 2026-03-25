import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  output: "export",
  sassOptions: {
    includePaths: ["./src"],
  },
};

export default nextConfig;
