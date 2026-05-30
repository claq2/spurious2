import { render, screen } from "@testing-library/react";
import { describe, expect, it, vi } from "vitest";
import MapComponent from "./MapComponent";

vi.mock("react-azure-maps", () => ({
  AzureMap: () => <div data-testid="azure-map" />,
}));

vi.mock("azure-maps-control", () => ({
  AuthenticationType: {
    anonymous: "anonymous",
  },
}));

describe("MapComponent", () => {
  it("renders the Azure map container", () => {
    render(<MapComponent />);

    expect(screen.getByTestId("azure-map")).toBeInTheDocument();
  });
});
