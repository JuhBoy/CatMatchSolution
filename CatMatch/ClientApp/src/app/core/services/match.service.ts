import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { CatMatch } from "../models/cat-match.model";
import { retry } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { CommonResponse } from "../models/common.response.model";

@Injectable()
export class MatchService {

    public constructor(private http: HttpClient) {}

    public getMatch(): Observable<CommonResponse<CatMatch>> {
        const url: string = `${environment.api.host}${environment.api.paths.match.getMatch}`;
        return this.http.get<CommonResponse<CatMatch>>(url).pipe(retry(3));
    }

    public resolveMatch(leftId: number, rightId: number, winnerId: number): Observable<any> {
        let url: string = `${environment.api.host}${environment.api.paths.match.resovleMatch}`;
        url += `/${leftId.toString()}/${rightId.toString()}/${winnerId.toString()}`;
        return this.http.get<any>(url).pipe(retry(3));
    }
}