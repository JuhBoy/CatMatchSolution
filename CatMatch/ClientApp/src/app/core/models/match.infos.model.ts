import { Match } from "./match.model";

export interface MatchInfos {
    id: number;
    matchCount: number;
    victories: number;
    history: Match[];
}