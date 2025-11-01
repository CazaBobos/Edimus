import { BiMenu } from "react-icons/bi";

import styles from "./styles.module.scss";

export const AdminHeader = () => {
  return (
    <div className={styles.title}>
      <BiMenu size={48} />
      <h1>Ēdimus - Admin</h1>
    </div>
  );
};
