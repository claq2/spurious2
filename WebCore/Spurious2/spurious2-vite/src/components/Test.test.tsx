import { render, screen } from "@testing-library/react";
import { describe, expect, it, vi } from "vitest";
import Test from "./Test";

vi.mock("react-router-dom", async () => {
  const actual =
    await vi.importActual<typeof import("react-router-dom")>(
      "react-router-dom",
    );
  return {
    ...actual,
    useRouteLoaderData: () => [
      { shortName: "beer", title: "Beer", address: "" },
    ],
  };
});

describe("Test component", () => {
  it("renders the test marker", () => {
    render(<Test />);

    expect(screen.getByText("Test")).toBeInTheDocument();
  });
});
