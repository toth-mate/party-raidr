export interface LocationState {
  lat: number | undefined;
  lng: number | undefined;
  loadLocation: () => Promise<void>;
}
