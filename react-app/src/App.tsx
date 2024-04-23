import React, { useState, useEffect } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';


interface Livro {
  isbn: string;
  nome: string;
  idAutor: number;
  preco: number;
  estado: boolean;
}

interface Autor {
  idAutor: number;
  nome: string;
}


function App() {

  const [livroSelecionado, setLivroSelecionado] = useState({
    isbn: '',
    nome: '',
    idAutor: '',
    preco: '',
    estado: true,
  })

  const [autorSelecionado, setAutorSelecionado] = useState({
    idAutor: '',
    nome: ''
  })

  const livroDTO = {
    isbn: livroSelecionado.isbn,
    nome: livroSelecionado.nome,
    idAutor: parseInt(livroSelecionado.idAutor),
    preco: parseFloat(livroSelecionado.preco),
    estado: livroSelecionado.estado
  };

  const autorDTO = {
    // idAutor: parseInt(autorSelecionado.idAutor),
    nome: autorSelecionado.nome
  };

  const baseLivroUrl = "http://localhost:5130/api/livros";

  const criarLivroUrl = "http://localhost:5130/api/livros/criarLivro";

  const editarLivroUrl = "http://localhost:5130/api/livros/atualizarLivro";

  /*   const ativarLivroUrl = `http://localhost:5130/api/livros/ativarLivro/${livroSelecionado.isbn}`; */

  const ativarLivroUrl = `http://localhost:5130/api/livros/ativarLivro/${livroDTO.isbn}`;

  const apagarLivroUrl = `http://localhost:5130/api/livros/apagarLivro/${livroDTO.isbn}`;

  const baseAutorUrl = "http://localhost:5130/api/autores";

  const criarAutorUrl = "http://localhost:5130/api/autores/criarAutor";

  const editarAutorUrl = `http://localhost:5130/api/autores/atualizarAutor/${autorSelecionado.idAutor}`;

  const apagarAutorUrl = `http://localhost:5130/api/autores/apagarAutor/${autorSelecionado.idAutor}`;

  const [data, setData] = useState<Livro[]>([]);
  const [livros, setLivros] = useState<Livro[]>([]);
  const [autores, setAutores] = useState<Autor[]>([]);
  const [updateData, setUpdateData] = useState(true);
  const [modalInserirLivro, setModalInserirLivro] = useState(false);
  const [modalInserirAutor, setModalInserirAutor] = useState(false);
  const [modalEditarLivro, setModalEditarLivro] = useState(false);
  const [modalEditarAutor, setModalEditarAutor] = useState(false);
  const [modalApagarLivro, setModalApagarLivro] = useState(false);
  const [modalApagarAutor, setModalApagarAutor] = useState(false);

  const [searchInput, setSearchInput] = useState('');
  const [filtroLivros, setFiltroLivros] = useState([]);
  const [filtroAutores, setFiltroAutores] = useState([]);  const [isLivro, setIsLivro] = useState(true);
  const [livrosAtivos, setLivrosAtivos] = useState([]);
  const [livrosInativos, setLivrosInativos] = useState([]);
  const [isbnInput, setIsbnInput] = useState("");
  const [livroPesquisado, setLivroPesquisado] = useState/* <Livro> */({
    isbn: '',
    nome: '',
    idAutor: '',
    preco: '',
    estado: true,
  });

  const selecionarLivro = (livro, opcao) => {
    setIsLivro(true);
    setLivroSelecionado(livro);
    (opcao === "Editar") ?
      abrirFecharModalEditarLivro() : abrirFecharModalApagarLivro();
  }

  const selecionarAutor = (autor, opcao) => {
    setIsLivro(false);
    setAutorSelecionado(autor);
    (opcao === "Editar") ?
      abrirFecharModalEditarAutor() : abrirFecharModalApagarAutor();
  }

  const abrirFecharModalInserirLivro = () => {
    setModalInserirLivro(!modalInserirLivro);
  }

  const abrirFecharModalInserirAutor = () => {
    setModalInserirAutor(!modalInserirAutor);
  }

  const abrirFecharModalEditarLivro = () => {
    setModalEditarLivro(!modalEditarLivro);
  }

  const abrirFecharModalEditarAutor = () => {
    setModalEditarAutor(!modalEditarAutor);
  }

  const abrirFecharModalApagarLivro = () => {
    setModalApagarLivro(!modalApagarLivro);
  }

  const abrirFecharModalApagarAutor = () => {
    setModalApagarAutor(!modalApagarAutor);
  }


  const handleLivroChange = /* (e: React.ChangeEvent<HTMLInputElement>) */ e => {
    const { name, value } = e.target;
    setLivroSelecionado({
      ...livroSelecionado, [name]: value
    });
    console.log(livroSelecionado);
  }

  const handleAutorChange = e => {
    const { name, value } = e.target;
    setAutorSelecionado({
      ...autorSelecionado,
      [name]: value
    });
    console.log(autorSelecionado);
    setLivroSelecionado({
      ...livroSelecionado,
      idAutor: value
    });
  };

  /* const handleInserirLivro = () => {
    axios.post(criarLivroUrl, livroSelecionado)
      .then(response => {
        console.log(response.data.obj);
        setLivroSelecionado({
          isbn: '',
          nome: '',
          idAutor: '',
          preco: ''
        });
        setModalInserirLivro(false);
      })
      .catch(error => {
        console.log(error);
      });
  } */


  const handleInputChange = (event) => {
    setIsbnInput(event.target.value);
  };

  const pedidoGetLivro = async () => {
    await axios.get(baseLivroUrl)
      .then(response => {
        setData(response.data.obj);
        setLivros(response.data.obj);
      }).catch(error => {
        console.log(error);
      })
  }

  const pedidoGetAutor = async () => {
    await axios.get(baseAutorUrl)
      .then(response => {
        setData(response.data.obj);
        setAutores(response.data.obj);
      }).catch(error => {
        console.log(error);
      })
  }

  const pedidoPostLivro = async () => {
    await axios.post(criarLivroUrl, livroDTO)
      .then(response => {
        setLivroSelecionado({
          isbn: '',
          nome: '',
          idAutor: '',
          preco: '',
          estado: true,
        });
        setData(data.concat(response.data.obj));
        abrirFecharModalInserirLivro();
        setModalInserirLivro(false);
        setUpdateData(true);
      }).catch(error => {
        console.log(error);
      });
  }

  const pedidoPostAutor = async () => {
    await axios.post(criarAutorUrl, autorDTO)
      .then(response => {
        setData(data.concat(response.data.obj));
        setUpdateData(true);
        abrirFecharModalInserirAutor();
      }).catch(error => {
        console.log(error);
      });
  }

  const pedidoPatchLivro = async () => {
    await axios.patch(editarLivroUrl, livroDTO)
      .then(response => {
        var resposta = response.data.obj;
        var dadosAuxiliar = data;
        dadosAuxiliar.forEach(livro => {
          if (livro.isbn === livroSelecionado.isbn) {
            livro.nome = resposta.nome;
            livro.idAutor = resposta.idAutor;
            livro.preco = resposta.preco;
          }
        });
        setUpdateData(true);
        abrirFecharModalEditarLivro();
      }).catch(error => {
        console.log(error);
      })
  }

  const pedidoPatchAutor = async () => {
    const autorDTOId = {
      ...autorDTO,
      idAutor: autorSelecionado.idAutor
    };
    await axios.patch(editarAutorUrl, autorDTOId)
      .then(response => {
        var resposta = response.data.obj;
        var dadosAuxiliar = autores.map(autor => {
          if (autor.idAutor === resposta.idAutor) {
            return resposta;
          } else {
            return autor;
          }
        });
        setAutores(dadosAuxiliar);
        setUpdateData(true);
        abrirFecharModalEditarAutor();
      }).catch(error => {
        console.log(error);
      });
  }

  const pedidoDeleteLivro = async () => {
    await axios.delete(apagarLivroUrl)
      // await axios.delete(apagarLivroUrl, livroDTO.isbn)

      /* .then(response => {
        setData(data.filter(livro => livro.isbn !== response.data.obj));
        setUpdateData(true);
        abrirFecharModalApagarLivro();
      }).catch(error => {
        console.log(error);
      }) */

      // Em vez de apagar o livro, o estado muda de 1 para 0, ou seja, para inativo
      .then(response => {
        setData(data.map(livro =>
          livro.isbn === response.data.obj ? { ...livro, Estado: 0 } : livro
        ));
        setUpdateData(true);
        abrirFecharModalApagarLivro();
      }).catch(error => {
        console.log(error);
      })
  }

  const pedidoDeleteAutor = async () => {
    await axios.delete(apagarAutorUrl)
      .then(response => {
        setData(data.filter(autor => autor.idAutor !== response.data.obj));
        setUpdateData(true);
        abrirFecharModalApagarAutor();
      }).catch(error => {
        console.log(error);
        console.log(autorSelecionado.idAutor);
      })
  }

  const searchLivros = (searchValue) => {
    setSearchInput(searchValue);
    if (searchInput !== '') {
      const dadosFiltrados = livros.filter((item) => {
        return Object.values(item).join('').toLowerCase()
          .includes(searchInput.toLowerCase())
      });
      setFiltroLivros(dadosFiltrados);
    }
    else {
      setFiltroLivros(livros);
    }
  }

  const searchAutores = (searchValue) => {
    setSearchInput(searchValue);
    if (searchInput !== '') {
      const dadosFiltrados = autores.filter((item) => {
        return Object.values(item).join('').toLowerCase()
          .includes(searchInput.toLowerCase())
      });
      setFiltroAutores(dadosFiltrados);
    }
    else {
      setFiltroAutores(autores);
    }
  }

  const ativarLivro = async (isbn) => {
    console.log(isbn);
    await axios.put(`http://localhost:5130/api/livros/ativarLivro/${isbn}`, { isbn: isbn })
      .then(response => {
        setLivros(livros.map(livro => livro.isbn === isbn ? { ...livro, estado: true } : livro));
        setUpdateData(true);
      })
      .catch(error => {
        console.error('Erro ao ativar livro:', error);
      });
  };

  const pesquisarLivroPorISBN = async (event) => {
    event.preventDefault();
    console.log(isbnInput);
    try {
      const response = await axios.get(`http://localhost:5130/api/livros/${isbnInput}`);
      console.log("Resposta da pesquisa:", response.data.obj);
      setLivroPesquisado(response.data.obj);
    } catch (error) {
      console.error("Erro ao pesquisar livro:", error);
    }
  };

 /*  useEffect(() => {
    axios.get(baseAutorUrl)
      .then(response => {
        setAutores(response.data.obj);
      })
      .catch(error => {
        console.log(error);
      });
  }, []);
  
  useEffect(() => {
    if (updateData) {
      pedidoGetLivro();
      setUpdateData(false);
    }
  }, [updateData]);
  
  useEffect(() => {
  }, [filtroAutores]);
  
  useEffect(() => {
  }, [filtroLivros]); */
  
  
  useEffect(() => {
    if (updateData) {
      pedidoGetLivro();
      pedidoGetAutor();
      setUpdateData(false);
    }
    axios.get(baseAutorUrl)
      .then(response => {
        setAutores(response.data.obj);
      })
      .catch(error => {
        console.log(error);
      });
  }, [updateData]);

  return (
    <div className="container">
      <div className='livro-container'>
        <br />
        <h3>Livros</h3>
        <header>
          <button className='btn btn-success' onClick={() => abrirFecharModalInserirLivro()}>Inserir livro</button>
        </header>
        <form>
          <br />
          <div className="input-container row">
            <div className="col-md-6">
              <input
                type="text"
                className="form-control"
                placeholder="Pesquisar por nome"
                onChange={(e) => searchLivros(e.target.value)}
              />
            </div>
            <div className="col-md-4">
              <input
                className="form-control"
                type="text"
                placeholder="Insira o ISBN"
                value={isbnInput}
                onChange={handleInputChange}
              />
            </div>
            <div className="col-md-2">
              <button
                className="btn btn-success"
                onClick={(event) => pesquisarLivroPorISBN(event)}
                /* onClick={pesquisarLivroPorISBN} */>Pesquisar</button>
            </div>
          </div>
        </form>
        <table className='table table-bordered'>
          <thead>
            <tr>
              <th>ISBN</th>
              <th>Nome</th>
              <th>Autor</th>
              <th>Preço</th>
              <th>Operação</th>
            </tr>
          </thead>
          <tbody>

            {livroPesquisado.isbn && (
              <tr key={livroPesquisado.isbn}>
                <td>{livroPesquisado.isbn}</td>
                <td>{livroPesquisado.nome}</td>
                <td>{livroPesquisado.idAutor}</td>
                <td>{livroPesquisado.preco}</td>
                <td>
                  <button
                    className='btn btn-primary'
                    onClick={() => selecionarLivro(livroPesquisado, "Editar")}
                  >
                    Editar
                  </button>{" "}
                  <button
                    className='btn btn-danger'
                    onClick={() => selecionarLivro(livroPesquisado, "Apagar")}
                  >
                    Apagar
                  </button>
                </td>
              </tr>
            )}

            {!livroPesquisado.isbn && (filtroLivros.length > 0 ? (
              filtroLivros.map(livro => (
                livro.estado ? (
                  <tr key={livro.isbn}>
                    <td>{livro.isbn}</td>
                    <td>{livro.nome}</td>
                    <td>{livro.idAutor}</td>
                    <td>{livro.preco}</td>
                    <td>
                      <button
                        className='btn btn-primary'
                        onClick={() => selecionarLivro(livro, "Editar")}
                      >
                        Editar
                      </button>{" "}
                      <button
                        className='btn btn-danger'
                        onClick={() => selecionarLivro(livro, "Apagar")}
                      >
                        Apagar
                      </button>
                    </td>
                  </tr>
                ) : null
              ))
            ) : (
              livros.filter(livro => livro.estado).map(livro => (
                <tr key={livro.isbn}>
                  <td>{livro.isbn}</td>
                  <td>{livro.nome}</td>
                  <td>{livro.idAutor}</td>
                  <td>{livro.preco}</td>
                  <td>
                    <button
                      className='btn btn-primary'
                      onClick={() => selecionarLivro(livro, "Editar")}
                    >
                      Editar
                    </button>{" "}
                    <button
                      className='btn btn-danger'
                      onClick={() => selecionarLivro(livro, "Apagar")}
                    >
                      Apagar
                    </button>
                  </td>
                </tr>
              ))
            ))}
          </tbody>

        </table>
      </div>

      <div className='livro-container'>
        <br />
        <h3>Livros Inativos</h3>
        <table className='table table-bordered'>
          <thead>
            <tr>
              <th>ISBN</th>
              <th>Nome</th>
              <th>Autor</th>
              <th>Preço</th>
              <th>Operação</th>
            </tr>
          </thead>
          <tbody>
          {livros.filter(livro => !livro.estado).map(livro => (
              <tr key={livro.isbn}>
                <td>{livro.isbn}</td>
                <td>{livro.nome}</td>
                <td>{livro.idAutor}</td>
                <td>{livro.preco}</td>
                <td>
                  <button className='btn btn-success' onClick={() => ativarLivro(livro.isbn)} /* onClick={() => handleAtivarLivro( livro.isbn )} */>Ativar</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className='autor-container'>
        <br />
        <h3>Autores</h3>
        <header>
          <button className='btn btn-success' onClick={() => abrirFecharModalInserirAutor()}>Inserir autor</button>
        </header>
        <form>
          <br />
          <input type="text"
            className='form-control'
            placeholder='Pesquisar por nome'
            onChange={(e) => searchAutores(e.target.value)}
          />
        </form>
        <table className='table table-bordered'>
          <thead>
            <tr>
              <th>Id</th>
              <th>Nome</th>
              <th>Operação</th>
            </tr>
          </thead>
          {searchInput.length > 0 ? (
            <tbody>
              {filtroAutores.map(autor => (
                <tr key={autor.idAutor}>
                  <td>{autor.idAutor}</td>
                  <td>{autor.nome}</td>
                  <td>
                    <button className='btn btn-primary' onClick={() => selecionarAutor(autor, "Editar")}>Editar</button> {"  "}
                    <button className='btn btn-danger' onClick={() => selecionarAutor(autor, "Apagar")}>Apagar</button>
                  </td>
                </tr>
              ))}
            </tbody>
          ) : (
            <tbody>
              {autores.map(autor => (
                <tr key={autor.idAutor}>
                  <td>{autor.idAutor}</td>
                  <td>{autor.nome}</td>
                  <td>
                    <button className='btn btn-primary' onClick={() => selecionarAutor(autor, "Editar")}>Editar</button> {"  "}
                    <button className='btn btn-danger' onClick={() => selecionarAutor(autor, "Apagar")}>Apagar</button>
                  </td>
                </tr>
              ))}
            </tbody>
          )}
        </table>
      </div>

      <Modal isOpen={modalInserirLivro}>
        <ModalHeader>Inserir Livro</ModalHeader>
        <ModalBody>
          <div className='form-group'>
            <label>ISBN: </label>
            <br />
            <input type="text" className='form-control' name="isbn" onChange={handleLivroChange} /* value={livroDTO.isbn} */ />
            <br />
            <label>Nome: </label>
            <br />
            <input type="text" className='form-control' name="nome" onChange={handleLivroChange} /* value={livroDTO.nome} */ />
            <br />
            <label>Autor: </label>
            <br />
            <select className='form-control' name="idAutor" onChange={handleAutorChange} /* value={livroDTO.idAutor} */>
              <option value="">Selecione o autor</option>
              {autores.map(autor => (
                <option key={autor.idAutor} value={autor.idAutor}>{autor.nome}</option>
              ))}
            </select>
            <br />
            <label>Preço: </label>
            <br />
            <input type="text" className='form-control' name="preco" onChange={handleLivroChange} /* value={livroDTO.preco} */ />
            <br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className='btn btn-primary' onClick={() => pedidoPostLivro()}/*  {... handleInserirLivro} */>Inserir</button>
          <button className='btn btn-secondary' onClick={() => setModalInserirLivro(false)}>Cancelar</button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalInserirAutor}>
        <ModalHeader>Inserir Autor</ModalHeader>
        <ModalBody>
          <div className='form-group'>
            <label>Nome: </label>
            <br />
            <input type="text" className='form-control' name="nome" onChange={handleAutorChange} />
            <br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className='btn btn-primary' onClick={() => pedidoPostAutor()}>Inserir</button>{"   "}
          <button className='btn btn-danger' onClick={() => abrirFecharModalInserirAutor()}>Cancelar</button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalEditarLivro}>
        <ModalHeader>Editar Livro</ModalHeader>
        <ModalBody>
          <div className='form-group'>
            <label>ISBN: </label>
            <br />
            <input type="text" className='form-control' name="isbn" readOnly value={livroSelecionado.isbn} />
            <br />
            <label>Nome: </label>
            <br />
            <input type="text" className='form-control' name="nome" value={livroSelecionado.nome} onChange={handleLivroChange} />
            <br />
            <label>ID Autor: </label>
            <br />
            <select className='form-control' name="idAutor" onChange={handleAutorChange} value={livroSelecionado.idAutor}>
              <option value="">Selecione o autor</option>
              {autores.map(autor => (
                <option key={autor.idAutor} value={autor.idAutor}>{autor.nome}</option>
              ))}
            </select>
            <br />
            <label>Preço: </label>
            <br />
            <input type="text" className='form-control' name="preco" value={livroSelecionado.preco} onChange={handleLivroChange} />
            <br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className='btn btn-primary' onClick={() => pedidoPatchLivro()}>Editar</button>{"   "}
          <button className='btn btn-danger' onClick={() => abrirFecharModalEditarLivro()}>Cancelar</button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalEditarAutor}>
        <ModalHeader>Editar Autor</ModalHeader>
        <ModalBody>
          <div className='form-group'>
            <label>ID Autor: </label>
            <br />
            <input type="text" className='form-control' name="idAutor" readOnly value={autorSelecionado.idAutor} />
            <br />
            <label>Nome: </label>
            <br />
            <input type='text' className='form-control' name="nome" value={autorSelecionado.nome} onChange={handleAutorChange} />
            <br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className='btn btn-primary' onClick={() => pedidoPatchAutor()}>Editar</button>{"   "}
          <button className='btn btn-danger' onClick={() => abrirFecharModalEditarAutor()}>Cancelar</button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalApagarLivro}>
        <ModalBody>
          Confirma a exclusão do livro: {livroSelecionado.nome} ?
        </ModalBody>
        <ModalFooter>
          <button className='btn btn-danger' onClick={() => pedidoDeleteLivro()}> Sim </button>
          <button className='btn btn-secondary' onClick={() => abrirFecharModalApagarLivro()}> Não </button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalApagarAutor}>
        <ModalBody>
          Confirma a exclusão do autor: {autorSelecionado.nome} ?
        </ModalBody>
        <ModalFooter>
          <button className='btn btn-danger' onClick={() => pedidoDeleteAutor()}> Sim </button>
          <button className='btn btn-secondary' onClick={() => abrirFecharModalApagarAutor()}> Não </button>
        </ModalFooter>
      </Modal>

    </div >
  );
}
export default App;