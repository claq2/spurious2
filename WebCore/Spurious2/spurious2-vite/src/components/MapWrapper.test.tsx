import { render, screen } from "@testing-library/react";
import type { ReactNode } from "react";
import { describe, expect, it, vi } from "vitest";
import MapWrapper from "./MapWrapper";

vi.mock("react-azure-maps", () => ({
  AzureMapsProvider: ({ children }: { children: ReactNode }) => (
    <div data-testid="maps-provider">{children}</div>
  ),
}));

vi.mock("./MapController", () => ({
  default: ({ subdivisionId }: { subdivisionId: number | undefined }) => (
    <div data-testid="map-controller">{String(subdivisionId)}</div>
  ),
}));

describe("MapWrapper", () => {
  it("renders provider and controller", () => {
    render(<MapWrapper subdivisionId={55} />);

    expect(screen.getByTestId("maps-provider")).toBeInTheDocument();
    expect(screen.getByTestId("map-controller")).toHaveTextContent("55");
  });
});
