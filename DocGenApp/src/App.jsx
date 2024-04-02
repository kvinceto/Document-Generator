import { BrowserRouter, Route, Routes } from "react-router-dom";
import { ErrorBoundary } from "react-error-boundary";
import ErrorBoundaryPage from "./components/pages/ErrorBoundaryPage.jsx";
import "./App.css";
import Home from "./components/pages/Home";

function App() {
  return (
    <ErrorBoundary FallbackComponent={ErrorBoundaryPage}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
        </Routes>
      </BrowserRouter>
    </ErrorBoundary>
  );
}

export default App;
