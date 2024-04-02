import Navigation from "../singleComponents/Navigation.jsx";
import styles from "./Home.module.css";

function Home() {
  return (
    <section className={styles.homeSection}>
      <Navigation />
      <div className={styles.homeContainer}>
        <h1>Free site for generating invoices</h1>
        <p>Try it</p>
      </div>
    </section>
  );
}

export default Home;
