import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface Regiao {
    id?: number;
    nome: string;
    ativo: boolean;
    cidades: { id: number; nome: string; uf: string }[];
}

interface Cidade {
    id: number;
    nome: string;
    uf: string;
}

@Injectable({
    providedIn: 'root',
})
export class RegiaoService {
    private apiUrl = 'https://localhost:44397/api/regiao';
    private cidadeApiUrl = 'https://localhost:44397/api/cidade';

    constructor(private http: HttpClient) { }

    getRegioes(): Observable<Regiao[]> {
        return this.http.get<Regiao[]>(this.apiUrl);
    }

    getCidades(): Observable<Cidade[]> {
        return this.http.get<Cidade[]>(this.cidadeApiUrl);
    }

    getRegiaoById(id: number): Observable<Regiao> {
        return this.http.get<Regiao>(`${this.apiUrl}/${id}`);
    }

    createRegiao(regiao: Regiao): Observable<any> {
        return this.http.post(this.apiUrl, regiao);
    }

    updateRegiao(id: number, regiao: Regiao): Observable<any> {
        return this.http.put(`${this.apiUrl}/${id}`, regiao);
    }

    deleteRegiao(id: number): Observable<any> {
        return this.http.delete(`${this.apiUrl}/${id}`);
    }

    toggleAtivo(id: number, ativo: boolean): Observable<any> {
        return this.http.patch(`${this.apiUrl}/${id}/toggleAtivo`, { ativo });
    }

    exportarParaExcel(): void {
        this.http.get('https://localhost:44397/api/regiao/export', { responseType: 'blob' }).subscribe(
            (blob) => {
                const link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = 'Regioes.xlsx';
                link.click();
            },
            (error) => {
                console.error('Erro ao exportar Excel:', error);
            }
        );
    }

}
