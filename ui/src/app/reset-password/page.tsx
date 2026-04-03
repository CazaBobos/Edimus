"use client";

import { ResetPasswordCard } from "@/components/views/Admin/ResetPasswordCard";
import { useRouter, useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";

import styles from "../admin/styles.module.scss";

export default function ResetPassword() {
  const router = useRouter();
  const searchParams = useSearchParams();
  const [mounted, setMounted] = useState(false);

  useEffect(() => setMounted(true), []);

  const token = searchParams.get("token");
  const userId = Number(searchParams.get("userId"));

  if (!mounted) return null;

  if (!token || !userId) {
    router.replace("/admin");
    return null;
  }

  return (
    <div className={styles.loginPage}>
      <div className={styles.loginContent}>
        <div className={styles.loginBrand}>
          <h1>Ēdimus</h1>
          <p>Restablecer Contraseña</p>
        </div>
        <ResetPasswordCard userId={userId} token={token} />
      </div>
    </div>
  );
}
