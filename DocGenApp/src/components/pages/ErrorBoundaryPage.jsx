/* eslint-disable react/prop-types */
import styles from "./ErrorBoundaryPage.module.css";

function ErrorBoundaryPage({ error }) {
  return (
    <>
      <div className={styles.page}>
        <h2 className={styles.heading}>Something went wrong!</h2>
        <p className={styles.friendlyMessage}>Please, try again later</p>
        <pre className={styles.errorMessage}>{error.message}</pre>
        <a href="/" className={styles.link}>
          Return to Home
        </a>
      </div>
    </>
  );
}

export default ErrorBoundaryPage;
