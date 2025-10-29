import styles from "./styles.module.scss";

type SectorTagProps = {
  name: string;
  color: string;
};
export const SectorTag = (props: SectorTagProps) => {
  const { name, color } = props;

  return (
    <span
      className={styles.tag}
      style={{
        background: color,
      }}
    >
      {name}
    </span>
  );
};
