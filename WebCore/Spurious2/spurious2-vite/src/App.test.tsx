import { render, screen } from "@testing-library/react";
import { Provider } from "react-redux";
import { describe, expect, it, vi } from "vitest";

vi.mock("./pages/dataLoader", () => ({
  dataLoader: async () => [{ shortName: "about", name: "About" }],
}));

vi.mock("./pages/Home", () => ({
  default: () => <div>Home route</div>,
}));

import App from "./App";
import { store } from "./store";

describe("App", () => {
  it("renders the application heading", async () => {
    render(
      <Provider store={store}>
        <App />
      </Provider>,
    );

    expect(
      await screen.findByRole("heading", {
        name: /alcohol density per census subdivision/i,
      }),
    ).toBeInTheDocument();
  });
});
