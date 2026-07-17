export interface EventDisplayDto {
    id: string;
    title: string;
    description: string;
    startingDate: string;
    endingDate: string;
    city: string;
    placeName: string;
    category: string;
    authorName: string;
    room: number;
    tickerPrice: number;
    dateCreated: string;
    isActive: boolean;
    eventStatus: string;
};

export interface EventMarkerDto {
    id: string;
    title: string;
    startingDate: string;
    endingDate: string;
    address: string;
    latitude: number;
    longitude: number;
};