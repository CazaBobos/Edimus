import styles from "./styles.module.scss";

type TabsProps = {
  source: string[];
  active: number;
  onChange: (index: number) => void;
};
export const Tabs = (props: TabsProps) => {
  const { source, active, onChange } = props;
  return (
    <div className={styles.tabs}>
      {source.map((t, i) => (
        <div key={t} className={styles.tab} data-selected={active === i} onClick={() => onChange(i)}>
          {t}
        </div>
      ))}
    </div>
  );
};
