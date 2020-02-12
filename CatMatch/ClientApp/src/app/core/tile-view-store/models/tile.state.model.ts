import { Cat } from "../../models/cat.model";

export interface TileState {
    loading: boolean;
    error: boolean;
    count: number;
    cats: Cat[];
}