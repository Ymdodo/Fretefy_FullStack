import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormArray, FormGroup, Validators } from '@angular/forms';
import { RegiaoService } from './regiao.service';

@Component({
  selector: 'app-regiao',
  templateUrl: './regiao.component.html',
  styleUrls: ['./regiao.component.scss']
})



export class RegiaoComponent implements OnInit {
  regioes: any[] = [];
  cidadesDisponiveis: any[] = [];
  modalAberto = false;
  regiaoForm!: FormGroup;
  regiaoSelecionada: any = null;
  constructor(
    private regiaoService: RegiaoService,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.listarRegioes();
    this.inicializarFormulario();
  }

  listarRegioes() {
    this.regiaoService.getRegioes().subscribe((data) => {
      this.regioes = data;
    });
  }


  toggleAtivo(regiao: any) {

    const regiaoAtualizada = { ...regiao, ativo: !regiao.ativo };

    this.regiaoService.updateRegiao(regiaoAtualizada.id, regiaoAtualizada).subscribe(
      () => {
        regiao.ativo = !regiao.ativo;
        console.log("Status atualizado com sucesso!");
      },
      (error) => {
        console.error("Erro ao alterar status da região:", error);
        alert("Erro ao alterar o status da região.");
      }
    );
  }



  abrirCadastro(regiaoExistente?: any) {
    this.modalAberto = true;

    if (this.cidadesDisponiveis.length === 0) {
      this.regiaoService.getCidades().subscribe((cidades) => {
        this.cidadesDisponiveis = cidades;
      });
    }

    if (regiaoExistente) {
      this.regiaoSelecionada = regiaoExistente;

      this.regiaoForm = this.fb.group({
        id: [regiaoExistente.id],
        nome: [regiaoExistente.nome, Validators.required],
        cidades: this.fb.array([])
      });

      const cidadesArray = this.regiaoForm.get('cidades') as FormArray;

      regiaoExistente.cidades.forEach((cidadeAssociada: any) => {
        cidadesArray.push(this.fb.group({
          cidadeId: [cidadeAssociada.cidadeId],
          nome: [cidadeAssociada.cidade.nome, Validators.required],
          uf: [cidadeAssociada.cidade.uf]
        }));
      });
    } else {
      this.inicializarFormulario();
    }
  }



  fecharCadastro() {
    this.modalAberto = false;
  }

  inicializarFormulario() {
    this.regiaoForm = this.fb.group({
      nome: ['', Validators.required],
      cidades: this.fb.array([])
    });
  }

  adicionarCidade(): void {
    const cidades = this.regiaoForm.get('cidades') as FormArray;
    cidades.push(this.fb.group({
      nome: ['', Validators.required]
    }));
  }


  removerCidade(index: number) {
    const cidadesArray = this.regiaoForm.get('cidades') as FormArray;
    cidadesArray.removeAt(index);
  }

  salvarEdicao() {
    if (this.regiaoForm.invalid) return;

    let regiaoEditada = { ...this.regiaoForm.value };

    regiaoEditada.id = this.regiaoSelecionada?.id || regiaoEditada.id;
    regiaoEditada.ativo = this.regiaoSelecionada?.ativo ?? true;

    regiaoEditada.cidades = regiaoEditada.cidades.map((cidadeSelecionada: any) => {
      const cidadeEncontrada = this.cidadesDisponiveis.find(c => c.nome === cidadeSelecionada.nome);

      return {
        id: cidadeSelecionada.id || 0,
        regiaoId: regiaoEditada.id,
        cidadeId: cidadeEncontrada ? cidadeEncontrada.id : cidadeSelecionada.cidadeId,
        cidade: {
          id: cidadeEncontrada ? cidadeEncontrada.id : cidadeSelecionada.cidadeId,
          nome: cidadeEncontrada ? cidadeEncontrada.nome : cidadeSelecionada.nome,
          uf: cidadeEncontrada ? cidadeEncontrada.uf : cidadeSelecionada.uf
        }
      };
    });

    // console.log("📌 JSON Enviado para API:", JSON.stringify(regiaoEditada, null, 2));

    this.regiaoService.updateRegiao(regiaoEditada.id, regiaoEditada).subscribe(
      (response) => {

        alert(response?.message || 'Região atualizada com sucesso!');
        this.fecharCadastro();
        this.listarRegioes();
      },
      (error) => {
        console.error('❌ Erro ao atualizar a região:', error);
        alert('Ocorreu um erro ao atualizar a região.');
      }
    );
  }



  salvarRegiao() {
    if (this.regiaoForm.invalid) return;

    let regiao = this.regiaoForm.value;

    regiao.cidades = regiao.cidades.map((cidadeSelecionada: any) => {
      const cidadeEncontrada = this.cidadesDisponiveis.find(c => c.nome === cidadeSelecionada.nome);
      return {
        cidadeId: cidadeEncontrada ? Number(cidadeEncontrada.id) : 0,
        nome: cidadeSelecionada.nome,
        uf: cidadeEncontrada?.uf || ""
      };
    });

    console.log("📌 Enviando para API:", JSON.stringify(regiao, null, 2));

    this.regiaoService.createRegiao(regiao).subscribe(() => {
      alert('Região cadastrada com sucesso!');
      this.fecharCadastro();
      this.listarRegioes();
    }, error => {
      console.error('Erro ao cadastrar a região:', error);
      alert('Ocorreu um erro ao cadastrar a região.');
    });
  }

  deletarRegiao(id: number) {
    if (confirm("Tem certeza que deseja deletar esta região?")) {
      this.regiaoService.deleteRegiao(id).subscribe(() => {
        alert("Região deletada com sucesso!");
        this.listarRegioes();
      }, error => {
        console.error("Erro ao deletar a região:", error);
        alert("Ocorreu um erro ao deletar a região.");
      });
    }
  }

  exportarExcel(): void {
    this.regiaoService.exportarParaExcel();
  }

}
