import { render, screen } from "@testing-library/react";
import { describe, expect, it, vi } from "vitest";
import Home from "./Home";

vi.mock("react-router-dom", async () => {
  const actual =
    await vi.importActual<typeof import("react-router-dom")>(
      "react-router-dom",
    );
  return {
    ...actual,
    useParams: () => ({ id: "beer" }),
  };
});

vi.mock("../components/MapWrapper", () => ({
  default: ({ subdivisionId }: { subdivisionId: number | undefined }) => (
    <div data-testid="map-wrapper">{String(subdivisionId)}</div>
  ),
}));

vi.mock("../components/SubdivisionList", () => ({
  default: () => <div data-testid="subdivision-list">Subdivision List</div>,
}));

describe("Home", () => {
  it("renders map and subdivision list", () => {
    render(<Home />);

    expect(screen.getByTestId("map-wrapper")).toHaveTextContent("undefined");
    expect(screen.getByTestId("subdivision-list")).toBeInTheDocument();
  });
});
