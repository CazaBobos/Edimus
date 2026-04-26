import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  output: "export",
  images: {
    unoptimized: true,
  },
  sassOptions: {
    includePaths: ["./src"],
  },
};

export default nextConfig;
